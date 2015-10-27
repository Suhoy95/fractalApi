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
            var item = new Item(){
                Id = 1,
                Analogy = new int[1]{1},
                Sub = new int[1] { 1 },
                Sup = new int[1] { 1 },
                Title = "Fractal",
                Text = "Добро пожаловать!"
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
                    new Item[2]{item, item},
                    new Item[1]{item},
                    new Item[2]{item, item},
                    new Item[1]{item}
                },

                PageTitile = "Главная - Fractal",
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
