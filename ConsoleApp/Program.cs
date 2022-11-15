using System.Text;

var _p = 0;
var _pi1 = 0;
var _pi2 = 0;

var s0000 = new State("0000", 0, false, 0, false);
var s0100 = new State("0100", 0, true, 0, false);
var s0001 = new State("0001", 0, false, 0, true);
var s1100 = new State("1100", 1, true, 0, false);
var s0101 = new State("0101", 0, false, 0, true);
var s0011 = new State("0011", 0, false, 1, true);
var s1101 = new State("1101", 1, true, 0, true);
var s0111 = new State("0111", 0, true, 1, true);
var s1111 = new State("1111", 1, true, 1, true);

var state = s0000;

var stateCountPairs = new Dictionary<State, int>();
var generatedCount = 0;

var queueLength1 = 0;

var channelLength1 = 0;
var processedCount1 = 0;
var processedOnQueue1 = 0;
var declinedCount1 = 0;

var l1 = 0;

var queueLength2 = 0;

var channelLength2 = 0;
var processedCount2 = 0;
var processedOnQueue2 = 0;
var declinedCount2 = 0;

var l2 = 0;

var requestLength = 0;

var count = 1_000_000;
for (int i = 0; i < count; i++)
{
    var p = NextBoolean(_p);
    var pi1 = NextBoolean(_pi1);
    var pi2 = NextBoolean(_pi2);
    Action(p, pi1, pi2);
}

var sb = new StringBuilder();

sb.Append("States:{").AppendLine();
foreach (var pair in stateCountPairs)
{
    sb.Append(pair.Key.Name).Append(": ").Append(pair.Value).AppendLine();
}
sb.Append('}');

var message = sb.ToString();
Console.WriteLine(message);

void Action(bool p, bool pi1, bool pi2)
{
    if (state == s0000)
    {
        if (p)
        {
            state = s0000;
        }
        else
        {
            state = s0100;
        }
    }
    else if (state == s0100)
    {
        if (p && pi1)
        {
            state = s0100;
        }
        else if (p && !pi1)
        {
            state = s0001;
        }
        else if (!p && pi1)
        {
            state = s1100;
        }
        else if (!p && !pi1)
        {
            state = s0101;
        }
    }
    else if (state == s0001)
    {
        if (p && !pi2)
        {
            state = s0000;
        }
        else if (p && pi2)
        {
            state= s0001;
        }
        else if (!p && !pi2)
        {
            state = s0100;
        }
        else if (!p && pi2)
        {
            state = s0101;
        }
    }
    else if (state == s1100)
    {
        if (p && pi1)
        {
            state = s1100;
        }
        else if (!p && pi1)
        {
            state= s1100;
        }
        else if (p && pi1)
        {
            state = s0100;
        }
        else if (p && !pi1)
        {
            state = s0101;
        }
        else if (!p && !pi1)
        {
            state = s1101;
        }
    }
    else if (state == s0101)
    {
        if (p && pi1 && !pi2)
        {
            state = s0100;
        }
        else if (!p && !pi1 && !pi2)
        {
            state = s0101;
        }
        else if (p && pi1 && pi2)
        {
            state = s0101;
        }
        else if (p && !pi1 && !pi2)
        {
            state = s0001;
        }
        else if (p && !pi1 && pi2)
        {
            state = s0011;
        }
        else if (p && pi1 && !pi2)
        {
            state = s0111;
        }
        else if (!p && !pi1 && !pi2)
        {
            state = s1101;
        }
        else if (!p && pi1 && !pi2)
        {
            state = s1100;
        }
    }
    else if (state == s0011)
    {
        if (p && !pi2)
        {
            state = s0001;
        }
        else if (p && !pi1 && pi2)
        {
            state = s0011;
        }
        else if (!p && !pi2)
        {
            state = s0101;
        }
        else if (!p && !pi1 && pi2)
        {
            state = s0111;
        }
    }
    else if (state == s1101)
    {
        if (p && pi1 && !pi2)
        {
            state = s1100;
        }
        else if (!p && pi1 && !pi2)
        {
            state = s1100;
        }
        else if (p && !pi1 && !pi2)
        {
            state = s0101;
        }
        else if (p && !pi1 && pi2)
        {
            state = s0111;
        }
        else if (!p && !pi1 && pi2)
        {
            state = s1111;
        }
        else if (!p && !pi1 && !pi2)
        {
            state = s1101;
        }
        else if (p && pi1 && pi2)
        {
            state = s1101;
        }
        else if (!p && pi1 && pi2)
        {
            state = s1101;
        }
    }
    else if (state == s0111)
    {
        if (p && pi1 && !pi2)
        {
            state = s0101;
        }
        else if (p && !pi1 && !pi2)
        {
            state = s0011;
        }
        else if (p && !pi1 && pi2)
        {
            state = s0011;
        }
        else if (!p && !pi1 && !pi2)
        {
            state = s0111;
        }
        else if (!p && !pi1 && pi2)
        {
            state = s0111;
        }
        else if (p && pi1 && pi2)
        {
            state = s0111;
        }
        else if (!p && pi1 && !pi2)
        {
            state = s1101;
        }
        else if (!p && pi1 && pi2)
        {
            state = s1111;
        }
    }
    else if (state == s1111)
    {
        if (p && pi1 && !pi2)
        {
            state = s1101;
        }
        else if (!p && pi1 && !pi2)
        {
            state = s1101;
        }
        else if (p && !pi1 && !pi2)
        {
            state = s0111;
        }
        else if (p && !pi1 && pi2)
        {
            state = s0111;
        }
        else if (!p && !pi1 && pi2)
        {
            state = s1111;
        }
        else if (!p && !pi1 && pi2)
        {
            state = s1111;
        }
        else if (p && pi1 && pi2)
        {
            state = s1111;
        }
        else if (!p && pi1 && pi2)
        {
            state = s1111;
        }
    }
    var ql1 = state.QueueLength1;
    var ql2 = state.QueueLength2;
    var v1 = AsInt(state.IsProcessed1);
    var v2 = AsInt(state.IsProcessed2);

    queueLength1 += ql1;
    channelLength1 += v1;
    l1 += ql1 + v1;
    queueLength2 += ql2;
    channelLength2 += v2;
    l2 += ql2 + v2;
    requestLength += ql1 + v1 + ql2 + v2;
    IncrementStateCount(state);
}

int AsInt(bool b) => b ? 1 : 0;

bool NextBoolean(float p) => Random.Shared.NextDouble() < p;

int GetStateCountOrDefault(State state, int defaultValue = default)
{
    return stateCountPairs.TryGetValue(state, out var count) ? count : defaultValue;
}

void IncrementStateCount(State state)
{
    stateCountPairs[state] = GetStateCountOrDefault(state) + 1;
}

record struct State(string Name, int QueueLength1, bool IsProcessed1, int QueueLength2, bool IsProcessed2);