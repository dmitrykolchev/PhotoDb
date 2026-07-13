// <copyright file="StateMachine.cs" company="Dmitry Kolchev">
// Copyright (c) 2026 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Managed.LLama.Cpp;

public class StateMachine
{
    private readonly int _channelStartToken;
    private readonly int _channelEndToken;
    private readonly int _thinkToken;
    private readonly int _turnStartToken;
    private readonly int _turnEndToken;
    private readonly int _thoughtToken;
    private readonly int _newLine;

    private readonly LLamaVocabulary _vocabulary;

    public enum State
    {
        Initial = 0,
        StartChannel = 1,
        Thought = 2,
        StartOfThinking = 3,
        ThinkingInprogess = 4,
        EndOfThinking = 5,
        ResponseInprogress = 6,
        ResponseComplete = 7,
        EndOfGeneration = 8,
    }

    public StateMachine(LLamaModel model)
    {
        _turnStartToken = model.Vocabulary.GetTokenId("<|turn>");
        _turnEndToken = model.Vocabulary.GetTokenId("<turn|>");
        _channelStartToken = model.Vocabulary.GetTokenId("<|channel>");
        _channelEndToken = model.Vocabulary.GetTokenId("<channel|>");
        _thinkToken = model.Vocabulary.GetTokenId("<|think|>");
        _thoughtToken = model.Vocabulary.GetTokenId("thought");
        _newLine = model.Vocabulary.GetTokenId("\n");
        _vocabulary = model.Vocabulary;
    }

    public State PreviousState { get; private set; } = State.Initial;

    public State CurrentState { get; private set; } = State.Initial;

    public bool StateChanged => PreviousState != CurrentState;

    public void Reset()
    {
        PreviousState = State.Initial;
        CurrentState = State.Initial;
    }

    public void Process(int token)
    {
        PreviousState = CurrentState;
        if (PreviousState == State.Initial)
        {
            if (token == _channelStartToken)
            {
                CurrentState = State.StartChannel;
            }
            else
            {
                CurrentState = State.ResponseInprogress;
            }
        }
        else if (PreviousState == State.StartChannel && token == _thoughtToken)
        {
            CurrentState = State.Thought;
        }
        else if (PreviousState == State.Thought && token == _newLine)
        {
            CurrentState = State.StartOfThinking;
        }
        else if (PreviousState == State.StartOfThinking)
        {
            if (token == _channelEndToken)
            {
                CurrentState = State.EndOfThinking;
            }
            else
            {
                CurrentState = State.ThinkingInprogess;
            }
        }
        else if(PreviousState == State.ThinkingInprogess && token == _channelEndToken)
        {
            CurrentState = State.EndOfThinking;
        }
        else if (PreviousState == State.EndOfThinking)
        {
            if (_vocabulary.IsEOG(token))
            {
                CurrentState = State.EndOfGeneration;
            }
            else
            {
                CurrentState = State.ResponseInprogress;
            }
        }
        else if (PreviousState == State.ResponseInprogress)
        {
            if(_vocabulary.IsEOG(token) || token == _turnEndToken)
            {
                CurrentState = State.EndOfGeneration;
            }
        }
        else if (PreviousState == State.EndOfGeneration)
        {
            throw new InvalidOperationException("Current state is EndOfGeneration");
        }
        else
        {
            if(_vocabulary.IsEOG(token) || token == _turnEndToken)
            {
                CurrentState = State.EndOfGeneration;
            }
        }
    }
}
