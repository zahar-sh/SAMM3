using System.Text;

var P0000 = new State("P0000", 0, false, 0, false);
var P0100 = new State("P0100", 0, true, 0, false);
var P0001 = new State("P0001", 0, false, 0, true);
var P1100 = new State("P1100", 1, true, 0, false);
var P0101 = new State("P0101", 0, false, 0, true);
var P0011 = new State("P0011", 0, false, 1, true);
var P1101 = new State("P1101", 1, true, 0, true);
var P0111 = new State("P0111", 0, true, 1, true);
var P1111 = new State("P1111", 1, true, 1, true);

var stateCountPairs = new Dictionary<State, int>();
var random = new Random();

var count = 1_000_000;

var state = P0000;

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

var _p = 0.3f;
var _pi1 = 0.55f;
var _pi2 = 0.65f;

Run();

var pairs = DivideValues(stateCountPairs, count);
var sum = pairs.Values.Sum();
var a = (processedCount1 + processedCount2) / (double)count;
var lq1 = queueLength1 / (double)count;
var lq2 = queueLength2 / (double)count;
var lc1 = requestLength/ (double)count;
var wq1 = processedOnQueue1 == 0 ? 0 : ((double)queueLength1 / processedOnQueue1);
var wq2 = processedOnQueue2 == 0 ? 0 : ((double)queueLength2 / processedOnQueue2);
var wq = wq1 + wq2;
var wc = (double)channelLength1 / processedCount1 + (double)channelLength2 / processedCount2 + wq1 + wq2;
var q = (double)processedCount2 / generatedCount;
var p = 1 - q;
var k1 = (double)channelLength1 / count;
var k2 = (double)channelLength2 / count;

var message = 
$@"States: {StatePairsToString(pairs)}
Sum: {sum}
A: {a}
Lq1: {lq1}
Lq2: {lq2}
Lc: {lc1}
Wq1: {wq1}
Wq2: {wq2}
Wq: {wq}
Wc: {wc}
Q: {q}
P: {p}
K1: {k1}
K2: {k2}";
Console.WriteLine(message);

void Run()
{
    for (int i = 0; i < count; i++)
    {
        var p = NextBoolean(_p);
        var pi1 = NextBoolean(_pi1);
        var pi2 = NextBoolean(_pi2);
        Action(p, pi1, pi2);
    }
}

void Action(bool p, bool pi1, bool pi2)
{
    if (state == P0000)
    {
        if (p)
        {
            state = P0000;
        }
        else
        {
            state = P0100;
        }
    }
    else if (state == P0100)
    {
        if (p && pi1)
        {
            state = P0100;
        }
        else if (p && !pi1)
        {
            state = P0001;
        }
        else if (!p && pi1)
        {
            state = P1100;
        }
        else if (!p && !pi1)
        {
            state = P0101;
        }
    }
    else if (state == P0001)
    {
        if (p && !pi2)
        {
            state = P0000;
        }
        else if (p && pi2)
        {
            state= P0001;
        }
        else if (!p && !pi2)
        {
            state = P0100;
        }
        else if (!p && pi2)
        {
            state = P0101;
        }
    }
    else if (state == P1100)
    {
        if (p && pi1)
        {
            state = P1100;
        }
        else if (!p && pi1)
        {
            state= P1100;
        }
        else if (p && pi1)
        {
            state = P0100;
        }
        else if (p && !pi1)
        {
            state = P0101;
        }
        else if (!p && !pi1)
        {
            state = P1101;
        }
    }
    else if (state == P0101)
    {
        if (p && pi1 && !pi2)
        {
            state = P0100;
        }
        else if (!p && !pi1 && !pi2)
        {
            state = P0101;
        }
        else if (p && pi1 && pi2)
        {
            state = P0101;
        }
        else if (p && !pi1 && !pi2)
        {
            state = P0001;
        }
        else if (p && !pi1 && pi2)
        {
            state = P0011;
        }
        else if (p && pi1 && !pi2)
        {
            state = P0111;
        }
        else if (!p && !pi1 && !pi2)
        {
            state = P1101;
        }
        else if (!p && pi1 && !pi2)
        {
            state = P1100;
        }
    }
    else if (state == P0011)
    {
        if (p && !pi2)
        {
            state = P0001;
        }
        else if (p && !pi1 && pi2)
        {
            state = P0011;
        }
        else if (!p && !pi2)
        {
            state = P0101;
        }
        else if (!p && !pi1 && pi2)
        {
            state = P0111;
        }
    }
    else if (state == P1101)
    {
        if (p && pi1 && !pi2)
        {
            state = P1100;
        }
        else if (!p && pi1 && !pi2)
        {
            state = P1100;
        }
        else if (p && !pi1 && !pi2)
        {
            state = P0101;
        }
        else if (p && !pi1 && pi2)
        {
            state = P0111;
        }
        else if (!p && !pi1 && pi2)
        {
            state = P1111;
        }
        else if (!p && !pi1 && !pi2)
        {
            state = P1101;
        }
        else if (p && pi1 && pi2)
        {
            state = P1101;
        }
        else if (!p && pi1 && pi2)
        {
            state = P1101;
        }
    }
    else if (state == P0111)
    {
        if (p && pi1 && !pi2)
        {
            state = P0101;
        }
        else if (p && !pi1 && !pi2)
        {
            state = P0011;
        }
        else if (p && !pi1 && pi2)
        {
            state = P0011;
        }
        else if (!p && !pi1 && !pi2)
        {
            state = P0111;
        }
        else if (!p && !pi1 && pi2)
        {
            state = P0111;
        }
        else if (p && pi1 && pi2)
        {
            state = P0111;
        }
        else if (!p && pi1 && !pi2)
        {
            state = P1101;
        }
        else if (!p && pi1 && pi2)
        {
            state = P1111;
        }
    }
    else if (state == P1111)
    {
        if (p && pi1 && !pi2)
        {
            state = P1101;
        }
        else if (!p && pi1 && !pi2)
        {
            state = P1101;
        }
        else if (p && !pi1 && !pi2)
        {
            state = P0111;
        }
        else if (p && !pi1 && pi2)
        {
            state = P0111;
        }
        else if (!p && !pi1 && pi2)
        {
            state = P1111;
        }
        else if (!p && !pi1 && pi2)
        {
            state = P1111;
        }
        else if (p && pi1 && pi2)
        {
            state = P1111;
        }
        else if (!p && pi1 && pi2)
        {
            state = P1111;
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

bool NextBoolean(float p) => random.NextDouble() < p;

int GetStateCountOrDefault(State state, int defaultValue = default)
{
    return stateCountPairs.TryGetValue(state, out var count) ? count : defaultValue;
}

void IncrementStateCount(State state)
{
    stateCountPairs[state] = GetStateCountOrDefault(state) + 1;
}

string StatePairsToString(IDictionary<State, double> statePairs)
{
    return ToString(statePairs.Select(pair => (pair.Key.Name, pair.Value.ToString())));
}

string ToString(IEnumerable<(string, string)> pairs)
{
    if (!pairs.Any())
        return "[]";
    var sb = new StringBuilder();
    sb.Append('[').AppendLine();
    foreach (var pair in pairs)
    {
        sb.Append('{').Append(pair.Item1).Append(':').Append(' ').Append(pair.Item2).Append('}').AppendLine();
    }
    return sb.Append(']').ToString();
}

IDictionary<T, double> DivideValues<T>(IDictionary<T, int> pairs, double v) where T : notnull
{
    var statePairs = new Dictionary<T, double>();
    foreach (var pair in pairs)
    {
        statePairs.Add(pair.Key, pair.Value / v);
    }
    return statePairs;
}

record class State(string Name, int QueueLength1, bool IsProcessed1, int QueueLength2, bool IsProcessed2);