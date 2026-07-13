// <copyright file="Program.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using PhotoDb.Data.Models;
using PhotoDb.Data.Models.Libraries;
using PhotoDb.Data.Services;
using PhotoDb.Data.Sqlite;
using Xobex.Cryptography;

//using PhotoDb.Data.SqlServer;

namespace PhotoDb.Data.Test;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var cryptoIdService = CryptoIdFactory.Create<long>(IdCipherAlgorithm.CompactDeterministicAes, "Hello World");
        var s1 = cryptoIdService.Encode(1);
        var s2 = cryptoIdService.Encode(2);
        var s3 = cryptoIdService.Encode(3);
        var s11 = cryptoIdService.Encode(1);
        var s12 = cryptoIdService.Encode(2);
        var s13 = cryptoIdService.Encode(3);

        var v1 = cryptoIdService.Decode(s1);
        var v11 = cryptoIdService.Decode(s11);
        var v2 = cryptoIdService.Decode(s2);
        var v12 = cryptoIdService.Decode(s12);
        var v3 = cryptoIdService.Decode(s3);
        var v13 = cryptoIdService.Decode(s13);

        //var builder = new DbContextOptionsBuilder<PhotoDbContext>();
        //builder.UseSqlServer("Server=localhost;Database=PhotoDb;Trusted_Connection=True;TrustServerCertificate=True");

        var builder = new DbContextOptionsBuilder<PhotoDbContext>();
        builder.UseSqlite("Data Source=photo.db");

        using var context = new PhotoDbContext(builder.Options);
        context.Database.EnsureCreated();
        var int32encoder = CryptoIdFactory.Create<int>(IdCipherAlgorithm.Speck32_64, "Hello World");
        var int64encoder = CryptoIdFactory.Create<long>(IdCipherAlgorithm.Speck64_128, "Hello World");

        var ls = new LibraryDataService(context, int32encoder);

        var library = await ls.CreateLibraryAsync(new CreateLibraryRequest { Name = "Canon 400D", Path = @"D:\Users\dykolchev.DYKBITS\Pictures\canon\400D" });
        //var result = await ls.ScanLibraryAsync(library.Id);
        //Console.WriteLine($"Added: {result.AddedCount}, Removed: {result.RemovedCount}, Total: {result.TotalCount} scanned in {result.ElapsedMilliseconds / 1000} sec.");

        //var libraries = await ls.GetLibrariesAsync();
        //for (var index = 0; index < libraries.Count; ++index)
        //{
        //    Console.WriteLine($"{libraries[index].Id} - {libraries[index].Name} ({libraries[index].ImageCount})");
        //}

        //var ims = new ImageDataService(context);
        //var images = await ims.GetImagesAsync(new GetImagesRequest
        //{
        //    //LibraryId = libraries[0].Id
        //});
        //TestInserts(context);
    }

    private static void TestInserts(PhotoDbContext context)
    {
        var library = context.Library.FirstOrDefault();
        if (library is null)
        {
            library = new Library()
            {
                Id = 1,
                State = 1,
                Name = "Test",
                Path = @"D:\Users\dykolchev.DYKBITS\Pictures\FB",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
            };

            context.Library.Add(library);
        }

        for (var row = 0; row < 100000; ++row)
        {
            var embedding = new float[256];

            for (var i = 0; i < embedding.Length; i++)
            {
                embedding[i] = Random.Shared.NextSingle();
            }

            var image = new Image()
            {
                State = 1,
                Name = "20180109_103707.jpg",
                FilePath = @"20180109_103707.jpg",
                Library = library,
                Flag = 0,
                Rating = 0,
                Size = 123,
                Width = 2,
                Height = 3,
                CameraModel = "Canon EOS 400D DIGITAL",
                LensModel = "EF-S10-22mm f/3.5-4.5 USM",
                FocalLength = "1000",
                Aperture = "2200",      // F * 100
                IsoSpeed = "40000",     // ISO * 100
                ExposureTime = "10000", // время в микросекундах
                ShootingDate = DateTime.Now,
                Hash = new byte[128],
                Embedding = embedding,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };

            context.Image.Add(image);

            if (row % 1000 == 999)
            {
                Console.Write("*");
                context.SaveChanges();
            }
        }
        context.SaveChanges();
    }
}
