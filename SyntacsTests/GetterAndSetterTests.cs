using System;
using Xunit;
using SyntacsApp.Models;


namespace SyntacsTests
{
    public class GetterAndSetterTests
    {
        [Theory]
        [InlineData("Bob",1,"Bob",1)]
        [InlineData("Steve",20,"Steve",20)]
        [InlineData("Jo",4,"Jo",4)]
        [InlineData("Alice",102,"Alice",102)]
        public void UserGetAndSetTests(string alias, int id, string expectedAlias, int expectedID)
        {
            User user = new User();
            user.Alias = alias;
            user.ID = id;
            Assert.Equal(expectedID, user.ID);
            Assert.Equal(expectedAlias, user.Alias);
        }
        [Theory]
        [InlineData(1,"Test Error",1,"Test Error")]
        [InlineData(5,"Error",5,"Error")]
        [InlineData(200,"Test",200,"Test")]
        [InlineData(15023,"NullRef",15023,"NullRef")]
        public void ErrorGetAndSetTests(int id, string name, int expectedID, string expectedName)
        {
            Error error = new Error();
            error.ID = id;
            error.DetailedName = name;

            Assert.Equal(expectedID, error.ID);
            Assert.Equal(expectedName, error.DetailedName);
        }
        [Theory]
        [InlineData(1, 5, 1, 5)]
        [InlineData(123, 77, 123, 77)]
        [InlineData(1024, 256, 1024, 256)]
        [InlineData(25, 15, 25, 15)]
        public void CommentGetAndSetTests(int id, int UserID, int expectedID, int expectedUserID)
        {
            Comment comment = new Comment();
            comment.ID = id;
            comment.UserID = UserID;

            Assert.Equal(expectedID, comment.ID);
            Assert.Equal(expectedUserID, comment.UserID);
        }
    }
}
