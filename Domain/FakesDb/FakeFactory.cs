using Domain.Abstract;
using Domain.Entities;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FakesDb
{
    public class FakeFactory
    {
        public static IGridRepository GetGridRepository()
        {
            var note = new Item(){
                id = 1,
                type = "note",
                analogy = new int[]{1,2,3},
                sub = new int[1] { 1 },
                sup = new int[1] { 1 },
                title = "Fractal",
                text = "Добро пожаловать!"
            };

            var gridItem = new Item(){
                id = 2,
                type = "gridItem",
                analogy = new int[] {1},
                sub = new int[]{},
                sup = new int[]{},
                slug = "subgrid",
                title = "Нужно больше листов",
                text = "Да, это фейк, но он уже должен быть!"
            };

            var article = new Item(){
                id = 3,
                type = "article",
                analogy = new int[] {1},
                sub = new int[]{},
                sup = new int[]{},
                slug = "test",
                title = "И статьи тоже нужны",
                text = "Больше, больше"
            };

            var fake = A.Fake<IGridRepository>();
            A.CallTo(() => fake.Get("main")).Returns(new Grid()
            {
                Slug = "main",
                Title = "Fractal",
                Width = 4,
                FixedWidth = true,
                Items = new Item[4][]
                {
                    new Item[]{note, note, note},
                    new Item[]{gridItem},
                    new Item[]{article, article, article},
                    new Item[]{note, article, gridItem}
                },

                PageTitle = "Главная - Fractal",
                PageDescription = "",
                PageKeywords = "",
            });
            return fake;
        }

        public static IArticleRepository GetArticleRepository()
        {
            return null;
        }

        public static INoteRepository GetNoteRepository()
        {
            return null;
        }
    }
}
