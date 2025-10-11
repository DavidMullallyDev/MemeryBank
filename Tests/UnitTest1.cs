namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange .... Declaration of variables, collecting the inputs
            MyMath mm = new();
            int input1 = 20, input2 = 5;
            int expected = 25;

            //Act ... calling the method to be tested
            int actual = mm.Add(input2, input1);

            //Assert ...compare actual/expected values
            Assert.Equal(expected, actual);
        }
    }
}
