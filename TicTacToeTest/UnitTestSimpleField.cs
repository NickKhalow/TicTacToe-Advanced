using TicTacToeCore.Data;
using TicTacToeCore.Field;
using Xunit;


namespace TicTacToeTest;

public class UnitTestSimpleField
{
    [Fact]
    public void TestWinning()
    {
        var field = new SimpleField(new[,]
        {
            {CellState.Free, CellState.Free, CellState.Free},
            {CellState.Free, CellState.Free, CellState.Free},
            {CellState.Free, CellState.Free, CellState.Free}
        });

        Assert.False(field.CheckGameFinished(out _));

        field = new SimpleField(new[,]
        {
            {CellState.OwnedX, CellState.OwnedX, CellState.OwnedX},
            {CellState.Free, CellState.Free, CellState.Free},
            {CellState.Free, CellState.Free, CellState.Free}
        });

        Assert.True(field.CheckGameFinished(out _));

        field = new SimpleField(new[,]
        {
            {CellState.Free, CellState.Free, CellState.Free},
            {CellState.OwnedX, CellState.OwnedX, CellState.OwnedX},
            {CellState.Free, CellState.Free, CellState.Free}
        });

        Assert.True(field.CheckGameFinished(out _));

        field = new SimpleField(new[,]
        {
            {CellState.Free, CellState.Free, CellState.Free},
            {CellState.Free, CellState.Free, CellState.Free},
            {CellState.OwnedX, CellState.OwnedX, CellState.OwnedX},
        });

        Assert.True(field.CheckGameFinished(out _));
        
        field = new SimpleField(new[,]
        {
            {CellState.Free, CellState.Free, CellState.Free},
            {CellState.Free, CellState.Free, CellState.Free},
            {CellState.OwnedX, CellState.Free, CellState.OwnedX},
        });

        Assert.False(field.CheckGameFinished(out _));
        
        field = new SimpleField(new[,]
        {
            {CellState.Free, CellState.OwnedY, CellState.Free},
            {CellState.Free, CellState.OwnedY, CellState.Free},
            {CellState.OwnedX, CellState.OwnedY, CellState.OwnedX},
        });

        Assert.True(field.CheckGameFinished(out _));
        
        field = new SimpleField(new[,]
        {
            {CellState.Free, CellState.Free, CellState.OwnedY},
            {CellState.Free, CellState.Free, CellState.OwnedY},
            {CellState.OwnedX, CellState.OwnedY, CellState.OwnedY},
        });

        Assert.True(field.CheckGameFinished(out _));
        
        field = new SimpleField(new[,]
        {
            {CellState.OwnedY, CellState.Free, CellState.OwnedY},
            {CellState.Free, CellState.OwnedY, CellState.OwnedX},
            {CellState.OwnedX, CellState.OwnedY, CellState.OwnedY},
        });

        Assert.True(field.CheckGameFinished(out _));
    }
}