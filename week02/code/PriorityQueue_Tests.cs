using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Dequeue from a queue with multiple items, where the highest priority item is at the end.
    // Expected Result: The value of the item with the highest priority ("Charlie") should be returned.
    // Defect(s) Found: The for loop in Dequeue had an incorrect boundary (index < _queue.Count - 1), causing it to skip the last element.
    public void Dequeue_HighestPriorityAtEnd_ShouldSucceed()
    {
        // Arrange
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Able", 2);
        priorityQueue.Enqueue("Baker", 5);
        priorityQueue.Enqueue("Charlie", 8);

        // Act
        var result = priorityQueue.Dequeue();

        // Assert
        Assert.AreEqual("Charlie", result);
    }

    [TestMethod]
    // Scenario: Dequeue from a queue containing items with the same highest priority.
    // Expected Result: The first item added with that priority ("Baker") should be returned, following FIFO rules.
    // Defect(s) Found: The comparison used was '>=', which caused a LIFO (Last-In, First-Out) behavior for tie-breaking instead of the required FIFO.
    public void Dequeue_TieBreaker_ShouldBeFIFO()
    {
        // Arrange
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Able", 2);
        priorityQueue.Enqueue("Baker", 8);
        priorityQueue.Enqueue("Charlie", 5);
        priorityQueue.Enqueue("Delta", 8);

        // Act
        var result = priorityQueue.Dequeue();

        // Assert
        Assert.AreEqual("Baker", result);
    }

    [TestMethod]
    // Scenario: Dequeue an item and then check the state of the queue.
    // Expected Result: The dequeued item should be removed from the queue.
    // Defect(s) Found: The Dequeue method found the correct item but never removed it from the internal list.
    public void Dequeue_ShouldRemoveItemFromQueue()
    {
        // Arrange
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Able", 2);
        priorityQueue.Enqueue("Baker", 8);
        
        // Act
        var result = priorityQueue.Dequeue();
        
        // Assert
        Assert.AreEqual("Baker", result);
        Assert.AreEqual("[Able (Pri:2)]", priorityQueue.ToString());
    }
    
    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: An InvalidOperationException should be thrown with the specified message.
    // Defect(s) Found: None. The original code correctly handles this scenario.
    public void Dequeue_EmptyQueue_ShouldThrowException()
    {
        // Arrange
        var priorityQueue = new PriorityQueue();

        // Act & Assert
        var ex = Assert.ThrowsException<System.InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items and perform multiple dequeues to ensure the queue remains in a valid state.
    // Expected Result: Each Dequeue call should return the item with the current highest priority.
    // Defect(s) Found: This test would fail due to all three identified defects (loop boundary, tie-breaker, and not removing the item).
    public void Dequeue_MultipleCalls_ShouldReturnHighestPriorityEachTime()
    {
        // Arrange
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Able", 5);
        priorityQueue.Enqueue("Baker", 5);
        priorityQueue.Enqueue("Charlie", 10);
        priorityQueue.Enqueue("Delta", 1);
        priorityQueue.Enqueue("Echo", 10);

        // Act & Assert
        Assert.AreEqual("Charlie", priorityQueue.Dequeue()); // First highest is Charlie (FIFO)
        Assert.AreEqual("Echo", priorityQueue.Dequeue());    // Second highest is Echo
        Assert.AreEqual("Able", priorityQueue.Dequeue());    // Third highest is Able (FIFO)
        Assert.AreEqual("Baker", priorityQueue.Dequeue());   // Fourth highest is Baker
        Assert.AreEqual("Delta", priorityQueue.Dequeue());   // Last item
    }
}