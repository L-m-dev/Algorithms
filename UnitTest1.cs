using Algorithms;

namespace TestCollection;

public class UnitTest1 {
    private HashStructure<int> _hashTable;
    public UnitTest1() {
        this._hashTable = new HashStructure<int>();
    }


    [Fact]
    public void AddElement() {
        _hashTable.Add("Nike", 25);
        Assert.Equal(25, _hashTable.Get("Nike"));
    } 
    
    [Fact]
    public void GetElement() {
        _hashTable.Add("Nike", 626);
        _hashTable.Add("Bert", 63223);
        _hashTable.Add("Bard", 6232);
        _hashTable.Add("Fate", 1235);
        var number = _hashTable.Get("Fate");
        Assert.Equal(1235, number);
    }  
    
    [Fact]
    public void UpdateElement() {
        _hashTable.Add("Nike", 25);
        _hashTable.Add("Nike", 50);
        Assert.Equal(50, _hashTable.Get("Nike"));
    }

    [Fact]
    public void RemoveElement() {
        _hashTable.Add("Nike", 25);
        _hashTable.Remove("Nike");
        Assert.Throws<KeyNotFoundException>(() =>
        {
            _hashTable.Get("Nike");
        });
    }
       
    [Fact]
    public void RemoveElement_WrongKey() {
        _hashTable.Add("Nike", 25);
        Assert.Throws<KeyNotFoundException>(() =>
        {
            _hashTable.Remove("wiequywoiryoiwyqoioqw");
        });
    }

    [Fact]
    public void AddElement_ShouldThrowInvalidKey() {

        Assert.ThrowsAny<ArgumentException>(() =>
        {
            _hashTable.Add("", 25);
        });
    }



}