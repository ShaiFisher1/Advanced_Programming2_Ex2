using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class RankingPageService
    {
        private static List<RankingPage> rankingPages = new List<RankingPage>();
        public List<RankingPage> GetAll() {
            return rankingPages;
        }

        public RankingPage Get(int id) {
            return rankingPages.Find(x => x.Id == id);
        }

        public void Edit(int id, string text, int ranking, string name, string time) {
            RankingPage rankingPage = Get(id);
            rankingPage.Name = name;
            rankingPage.Text = text;
            rankingPage.Ranking = ranking;
            rankingPage.Time = time;
        }

        public void Delete(int id) {
            rankingPages.Remove(Get(id));
        }

    }
}
