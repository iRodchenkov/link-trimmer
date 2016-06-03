using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iRodchenkov.Logic;
using System.Linq;

namespace iRodchenkov.LinkTrimmer.Tests
{
    [TestClass]
    public class LinkTrimmerTests
    {
        [TestInitialize]
        public void Init()
        {
            SecurityManager.Init(new DummyUserProvider());

            using (var context = new iRodchenkov.Data.DataContext())
            {
                context.Database.ExecuteSqlCommand(@"delete from LinkDatas");
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void TrimLink()
        {
            var url = @"https://github.com/iRodchenkov/link-trimmer";
            var badUrl = @"bar-url";

            using(var trimmer = new iRodchenkov.Logic.LinkTrimmer())
            {
                var r = new OperationResult();

                var r1 = trimmer.CreateLink(url, r);

                Assert.IsNotNull(r1);
                Assert.IsFalse(r.HasErrors);


                var r2 = trimmer.CreateLink(badUrl, r);

                Assert.IsNull(r2);
                Assert.IsTrue(r.HasErrors);
                Assert.AreEqual("Bad url format", r.AllErrors);
            }
        }

        [TestMethod]
        public void History()
        {
            var url = @"https://github.com/iRodchenkov/link-trimmer";
            var count = 7;
            var take = 5;

            using (var trimmer = new iRodchenkov.Logic.LinkTrimmer())
            {
                for (int i = 0; i < count; ++i)
                {
                    var r = new OperationResult();
                    trimmer.CreateLink(string.Format("{0}#{1}", url, i), r);
                    Assert.IsFalse(r.HasErrors);
                }

                int total;
                var page1 = trimmer.HistoryForCurrentUser(0, take, out total);

                Assert.AreEqual(take, page1.Length);
                Assert.AreEqual(count, total);

                var page2 = trimmer.HistoryForCurrentUser(take, take, out total);

                Assert.AreEqual(total - take, page2.Length);
                Assert.AreEqual(count, total);
            }
        }

        [TestMethod]
        public void Click()
        {
            var url = @"https://github.com/iRodchenkov/link-trimmer";
            var take = 5;

            using (var trimmer = new iRodchenkov.Logic.LinkTrimmer())
            {
                var r = new OperationResult();
                var link = trimmer.CreateLink(url, r);
                Assert.IsFalse(r.HasErrors);

                var id = int.Parse(link.TrimmedUrl.Split('/').Last());

                int total;
                var tLink = trimmer.HistoryForCurrentUser(0, take, out total)[0];

                Assert.AreEqual(url, tLink.SourceUrl);
                Assert.AreEqual(0, tLink.Clicks);

                
                var tUrl = trimmer.GetOriginalUrl(id);

                Assert.AreEqual(url, tUrl);


                tLink = trimmer.HistoryForCurrentUser(0, take, out total)[0];

                Assert.AreEqual(url, tLink.SourceUrl);
                Assert.AreEqual(1, tLink.Clicks);


                trimmer.GetOriginalUrl(id);
                trimmer.GetOriginalUrl(id);
                trimmer.GetOriginalUrl(id);
                trimmer.GetOriginalUrl(id);


                tLink = trimmer.HistoryForCurrentUser(0, take, out total)[0];

                Assert.AreEqual(url, tLink.SourceUrl);
                Assert.AreEqual(5, tLink.Clicks);
            }
        }
    }
}
