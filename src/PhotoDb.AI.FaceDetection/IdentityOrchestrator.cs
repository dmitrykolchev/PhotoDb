// <copyright file="IdentityOrchestrator.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace PhotoDb.AI.FaceDetection;

public class IdentityOrchestrator
{
    private const float ClusterMatchThreshold = 0.55f;
    private const float MinimumQualityForNewCluster = 18.0f; // Фильтрация мусорных/размытых лиц

    public List<KnownPerson> KnownPersons { get; } = [];

    public List<FaceCluster> Clusters { get; } = [];

    public MatchResult ProcessNewFace(FaceEmbedding face, int photoId, FaceBox bbox)
    {
        var query = face.Vector;

        // 1. Поиск по известным персонам (Closed-set)
        var personMatch = FindBestPersonMatch(query);
        if (personMatch.HasValue && personMatch.Value.Sim >= personMatch.Value.Person.AdaptiveThreshold)
        {
            SaveMatchToDatabase(photoId, bbox, personId: personMatch.Value.Person.Id, clusterId: null, face.QualityNorm);
            return new MatchResult.KnownMatch(personMatch.Value.Person.Name, personMatch.Value.Sim);
        }

        // 2. Поиск по центроидам кластеров неизвестных (Open-set)
        var clusterMatch = FindBestClusterMatch(query);
        if (clusterMatch.HasValue && clusterMatch.Value.Sim >= ClusterMatchThreshold)
        {
            var targetCluster = clusterMatch.Value.Cluster;
            targetCluster.AddFaceAndUpdateCentroid(face.Vector, face.QualityNorm);

            SaveMatchToDatabase(photoId, bbox, personId: null, clusterId: targetCluster.Id, face.QualityNorm);
            UpdateClusterInDatabase(targetCluster);

            return new MatchResult.ClusterMatch(targetCluster.Id, clusterMatch.Value.Sim);
        }

        // 3. Попытка создать новый кластер
        if (face.QualityNorm >= MinimumQualityForNewCluster)
        {
            var newClusterId = InsertNewClusterToDatabase(face.Vector.ToArray(), face.QualityNorm);
            var newCluster = new FaceCluster(newClusterId, face.Vector.ToArray(), face.QualityNorm);
            Clusters.Add(newCluster);

            SaveMatchToDatabase(photoId, bbox, personId: null, clusterId: newCluster.Id, face.QualityNorm);
            return new MatchResult.NewClusterCreated(newCluster.Id);
        }

        // 4. Лицо признано шумовым (низкий QualityNorm) - пишем в базу без привязки к сущностям
        SaveMatchToDatabase(photoId, bbox, personId: null, clusterId: null, face.QualityNorm);
        return new MatchResult.NoiseIgnored();
    }

    private (KnownPerson Person, float Sim)? FindBestPersonMatch(ReadOnlySpan<float> query)
    {
        KnownPerson? bestPerson = null;
        var maxSim = float.MinValue;

        foreach (var person in KnownPersons)
        {
            foreach (var refEmb in person.ReferenceEmbeddings)
            {
                var sim = MetricSpace.CosineSimilarityNormalized(query, refEmb);
                if (sim > maxSim) { maxSim = sim; bestPerson = person; }
            }
        }
        return bestPerson != null ? (bestPerson, maxSim) : null;
    }

    private (FaceCluster Cluster, float Sim)? FindBestClusterMatch(ReadOnlySpan<float> query)
    {
        FaceCluster? bestCluster = null;
        var maxSim = float.MinValue;

        foreach (var cluster in Clusters)
        {
            var sim = MetricSpace.CosineSimilarityNormalized(query, cluster.Centroid);
            if (sim > maxSim) { maxSim = sim; bestCluster = cluster; }
        }
        return bestCluster != null ? (bestCluster, maxSim) : null;
    }

    // Заглушки для интеграции с репозиторием БД
    protected virtual void SaveMatchToDatabase(int photoId, FaceBox box, int? personId, int? clusterId, float q)
    {
        Console.WriteLine(nameof(SaveMatchToDatabase));
    }

    protected virtual void UpdateClusterInDatabase(FaceCluster cluster)
    {
        Console.WriteLine(nameof(UpdateClusterInDatabase));
    }

    protected virtual int InsertNewClusterToDatabase(float[] centroid, float quality)
    {
        Console.WriteLine(nameof(InsertNewClusterToDatabase));
        return -1;
    }
}
