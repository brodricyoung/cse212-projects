using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Try to get a value from an empty queue
    // Expected Result: Exception should be thrown with appropriate error message
    // Defect(s) Found: None
    public void TestPriorityQueue_DequeueEmpty()
    {
        var priorityQueue = new PriorityQueue();

        try {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should've been thrown");
        }
        catch (InvalidOperationException e) {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Enqueue a value to an empty queue
    // Expected Result: [a (Pri:0)]
    // Defect(s) Found: None
    public void TestPriorityQueue_EnqueueEmpty()
    {
        var expectedResult = "[a (Pri:0)]";

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("a", 0);

        Assert.AreEqual(expectedResult, priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Enqueue values to a queue to ensure they're enqueued to the end
    // Expected Result: [a (Pri:0), b (Pri:1), c (Pri:2)]
    // Defect(s) Found: None
    public void TestPriorityQueue_Enqueue()
    {
        var expectedResult = "[a (Pri:0), b (Pri:1), c (Pri:2)]";

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("a", 0);
        priorityQueue.Enqueue("b", 1);
        priorityQueue.Enqueue("c", 2);


        Assert.AreEqual(expectedResult, priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Dequeue the item with the highest priority when the first is the first highest and there's more than one with that priority
    // Expected Result: a
    // Defect(s) Found: FOR loop in dequeue function started index at 1 instead of 0 and IF statement shoulve used > instead of >=
    public void TestPriorityQueue_DequeueFirst()
    {
        var expectedResult = "a";

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("a", 1);
        priorityQueue.Enqueue("b", 1);
        priorityQueue.Enqueue("c", 1);


        Assert.AreEqual(expectedResult, priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue the item with the highest priority when a middle item is the first highest and there's more than one with that priority
    // Expected Result: b
    // Defect(s) Found: None
    public void TestPriorityQueue_DequeueMiddle()
    {
        var expectedResult = "b";

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("a", 0);
        priorityQueue.Enqueue("b", 1);
        priorityQueue.Enqueue("c", 1);


        Assert.AreEqual(expectedResult, priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue the item with the highest priority when the last is the first highest
    // Expected Result: c
    // Defect(s) Found: Condition in the FOR loop shoulve used <= instead of <
    public void TestPriorityQueue_DequeueLast()
    {
        var expectedResult = "c";

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("a", 0);
        priorityQueue.Enqueue("b", 0);
        priorityQueue.Enqueue("c", 1);


        Assert.AreEqual(expectedResult, priorityQueue.Dequeue());
    }
}
