using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Project.Scripts;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class UserNameChangeUseCaseTest
    {
        public class MockPresentation : IUserNamePresentation
        {
            public bool isCalledShowChangeNameSuccess;
            public bool isCalledShowChangeNameFailure;
            public void ShowChangeNameSuccess(string newName)
            {
                isCalledShowChangeNameSuccess = true;
            }

            public void ShowChangeNameFailure(string failureName)
            {
                isCalledShowChangeNameFailure = true;
            }
        }
        
        private UserNameChangeUseCase _useCase;
        private MockPresentation _presentation;

        [SetUp]
        public void SetUp()
        {
            _presentation = new MockPresentation();
            _useCase = new UserNameChangeUseCase(_presentation);
        }
        
        [TearDown]
        public void TearDown()
        {
            PlayerPrefs.DeleteKey("user_name");
        }
        
        [Test]
        public void 名前変更のバリデーションが通ったときに名前が保存される()
        {
            // act
            _useCase.ChangeName("newname");
            
            // assert
            Assert.That(PlayerPrefs.GetString("user_name"), Is.EqualTo("newname"));
        }

        [Test]
        public void 名前変更のバリデーションが通らなかったときは名前は保存されない()
        {
            // act
            _useCase.ChangeName("あいうえお");
            
            // assert
            Assert.That(PlayerPrefs.GetString("user_name"), Is.Not.EqualTo("あいうえお"));
        }

        [Test]
        public void 名前変更のバリデーションが通ったときは成功したことを出力する()
        {
            _useCase.ChangeName("newname");
            Assert.That(_presentation.isCalledShowChangeNameSuccess, Is.True);
        }

        [Test]
        public void 名前変更のバリデーションが通らなかったときは失敗したことを出力する()
        {
            _useCase.ChangeName("だめなやつ");
            Assert.That(_presentation.isCalledShowChangeNameFailure, Is.True);
        }
    }
}
