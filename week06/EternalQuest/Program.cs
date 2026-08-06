using System;

/*
 * EXCEEDING REQUIREMENTS — what was added beyond the base spec:
 *
 * 1. Leveling/title system (GoalManager.GetLevel/GetTitle): every 500 points
 *    earned levels the user up, with a fun title ("Newcomer" through "Eternal
 *    Champion") displayed alongside player info and announced on level-up.
 *
 * 2. ProgressGoal: a fifth goal type for tracking incremental progress
 *    toward a large numeric target (e.g. "run 26 miles"), rather than a
 *    simple complete/incomplete or a fixed repeat count. Points scale with
 *    however much progress is recorded, plus a bonus on reaching the target.
 *
 * 3. NegativeGoal: a sixth goal type for habits the user is trying to break.
 *    Unlike every other goal, recording it costs points instead of earning
 *    them — the goal is to record it as little as possible.
 *
 * Both new goal types plug into the same polymorphic RecordEvent() /
 * GetDetailsString() / GetStringRepresentation() pattern as the three
 * required goal types, so GoalManager and GoalFactory needed no special-case
 * logic to support them.
 */

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}