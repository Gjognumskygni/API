using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace TingParser.Services.Tests
{
    [TestClass()]
    public class LogtingParserServiceTests
    {
        private string GetFileContent(string fileName)
        {
            var _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            _filePath = Directory.GetParent(_filePath).FullName;
            _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
            _filePath += $@"\TestData\{fileName}";
            TextReader tr = new StreamReader(_filePath);
            return tr.ReadToEnd();
        }

        [TestMethod()]
        public void ParseVoteHtmlContentTest()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("VotePage1.txt");
            Assert.IsNotNull(testData);

            var model = service.ParseVote(testData);
            Assert.IsNotNull(model);
            
            Assert.AreEqual("2019", model.Term);
            Assert.AreEqual("2", model.Reading);
            Assert.AreEqual("11", model.Topic);
            Assert.AreEqual(16, model.YesCount);
            Assert.AreEqual(8, model.NoCount);
            Assert.AreEqual(2, model.AbsentCount);
            Assert.AreEqual(7, model.BlankCount);
            Assert.AreEqual(31, model.Present);
            Assert.AreEqual(new DateTime(2019, 12, 10, 17, 06, 00), model.VoteDate);
        }

        [TestMethod()]
        public void ParseVoteHtmlContentTest2()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("VotePage2.txt");
            Assert.IsNotNull(testData);

            var model = service.ParseVote(testData);
            Assert.IsNotNull(model);
            
            Assert.AreEqual("2019", model.Term);
            Assert.AreEqual("2", model.Reading);
            Assert.AreEqual("135", model.Topic);
            Assert.AreEqual(19, model.YesCount);
            Assert.AreEqual(0, model.NoCount);
            Assert.AreEqual(14, model.AbsentCount);
            Assert.AreEqual(0, model.BlankCount);
            Assert.AreEqual(19, model.Present);
            Assert.AreEqual(new DateTime(2020, 04, 06, 21, 18, 00), model.VoteDate);
        }

        [TestMethod()]
        public void ParseOverviewTest()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("OverviewPage1.txt");
            Assert.IsNotNull(testData);

            var urls = new List<string> {
                "http://logting.fo/files/sound/2019/132/2/2020-132-2.html",
                "http://logting.fo//files/sound/votes/2019/135/2/vote_2020-135-2_04062020-211847.html",
                "http://logting.fo//files/sound/votes/2019/135/2/vote_2020-135-2_04062020-212045.html",
            };

            var parsedUrls = service.ParseOverviewForRowLinks(testData);
            Assert.IsNotNull(parsedUrls);
            Assert.AreEqual(urls.Count, parsedUrls.Count);
            urls.ForEach(x => Assert.IsTrue(parsedUrls.Contains(x)));
        }

        [TestMethod()]
        public void ParseOverviewTest2()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("OverviewPage2.txt");
            Assert.IsNotNull(testData);

            var urls = new List<string> {
                "https://logting.fo/files/casestate/28163/135.19%20Alit%20i%20ll.%20um%20studulsskipan%20til%20fyritokur%20til%20fastar%20utreidslur%20orsakad%20av%20COVID-19.pdf",
                "https://logting.fo/files/casestate/28163/135.19%20Skjal%20A.pdf",
                "https://logting.fo/files/casestate/28163/135.19%20Skjal%20B.pdf",
                "https://logting.fo/files/casestate/28163/135.19%20Skjal%20C.pdf",
                "https://logting.fo/files/casestate/28163/135.19%20Skjal%20D.pdf",
                "https://logting.fo/files/casestate/28163/135.19%20Skjal%20E.pdf",
            };

            var parsedUrls = service.ParseOverviewForRowLinks(testData);
            Assert.IsNotNull(parsedUrls);
            Assert.AreEqual(urls.Count, parsedUrls.Count);
            urls.ForEach(x => Assert.IsTrue(parsedUrls.Contains(x)));
        }


        [TestMethod()]
        public void ParseGetPaginationCountFromAdvancedSearch()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("AdvancedSearch1.txt");
            Assert.IsNotNull(testData);

            var paginationCount = service.ParseGetPaginationCountFromAdvancedSearch(testData);
            Assert.AreEqual(3, paginationCount);
        }

        [TestMethod()]
        public void ParseGetPaginationUrlFromAdvancedSearch()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("AdvancedSearch1.txt");
            Assert.IsNotNull(testData);

            var urls = new List<string> {
                "https://logting.fo/search/advancedSearch.gebs?d-16544-p=1&year=2013&subject=&parliamentMember=&menuChanged=%23parameters.menuChanged&lawNo=&committee.id=&caseType=-1",
                "https://logting.fo/search/advancedSearch.gebs?d-16544-p=2&year=2013&subject=&parliamentMember=&menuChanged=%23parameters.menuChanged&lawNo=&committee.id=&caseType=-1",
                "https://logting.fo/search/advancedSearch.gebs?d-16544-p=3&year=2013&subject=&parliamentMember=&menuChanged=%23parameters.menuChanged&lawNo=&committee.id=&caseType=-1",
            };

            var paginationUrls = service.ParseGetPaginationUrlFromAdvancedSearch(testData);
            Assert.AreEqual(urls.Count, paginationUrls.Count);

            foreach (var item in urls)
            {
                Assert.IsTrue(paginationUrls.Contains(item));
            }
        }

        [TestMethod()]
        public void ParseAdvancedSearchTest1()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("AdvancedSearch1.txt");
            Assert.IsNotNull(testData);

            var parsedUrls = service.ParseGetCaseUrlsFromAdvancedSearch(testData);

            Assert.IsTrue(parsedUrls.Count == 200);
        }

        [TestMethod()]
        public void ParseAdvancedSearchTest2()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("AdvancedSearch2.txt");
            Assert.IsNotNull(testData);

            var parsedUrls = service.ParseGetCaseUrlsFromAdvancedSearch(testData);

            Assert.IsTrue(parsedUrls.Count == 200);
        }

        [TestMethod()]
        public void ParseAdvancedSearchTest3()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("AdvancedSearch3.txt");
            Assert.IsNotNull(testData);

            var parsedUrls = service.ParseGetCaseUrlsFromAdvancedSearch(testData);

            Assert.IsTrue(parsedUrls.Count == 12);
        }

        [TestMethod()]
        public void ParseCaseType1()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("CaseType1.txt");
            Assert.IsNotNull(testData);

            var caseType = service.ParseCaseType(testData);
            Assert.AreEqual(CaseType.Nevndarmál, caseType);
        }

        [TestMethod()]
        public void ParseCaseType2()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("CaseType2.txt");
            Assert.IsNotNull(testData);

            var caseType = service.ParseCaseType(testData);
            Assert.AreEqual(CaseType.Figgjarlogaruppskot, caseType);
        }

        [TestMethod()]
        public void ParseCaseType3()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("CaseType3.txt");
            Assert.IsNotNull(testData);

            var caseType = service.ParseCaseType(testData);
            Assert.AreEqual(CaseType.Frágreiðing, caseType);
        }

        [TestMethod()]
        public void ParseCaseType4()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("CaseType4.txt");
            Assert.IsNotNull(testData);

            var caseType = service.ParseCaseType(testData);
            Assert.AreEqual(CaseType.Logaruppskot, caseType);
        }

        [TestMethod()]
        public void ParseCaseType5()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("CaseType5.txt");
            Assert.IsNotNull(testData);

            var caseType = service.ParseCaseType(testData);
            Assert.AreEqual(CaseType.Muntligar_Fyrispurningar, caseType);
        }

        [TestMethod()]
        public void ParseCaseType6()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("CaseType6.txt");
            Assert.IsNotNull(testData);

            var caseType = service.ParseCaseType(testData);
            Assert.AreEqual(CaseType.Rikistilmali, caseType);
        }

        [TestMethod()]
        public void ParseCaseType7()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("CaseType7.txt");
            Assert.IsNotNull(testData);

            var caseType = service.ParseCaseType(testData);
            Assert.AreEqual(CaseType.SkrivligarFyrispurningar, caseType);
        }

        [TestMethod()]
        public void ParseCaseType8()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("CaseType8.txt");
            Assert.IsNotNull(testData);

            var caseType = service.ParseCaseType(testData);
            Assert.AreEqual(CaseType.Spurningar52, caseType);
        }

        [TestMethod()]
        public void ParseCaseType9()
        {
            var service = new LogtingParserService();

            var testData = GetFileContent("CaseType9.txt");
            Assert.IsNotNull(testData);

            var caseType = service.ParseCaseType(testData);
            Assert.AreEqual(CaseType.UppskotTilSamtyktar, caseType);
        }
    }
}