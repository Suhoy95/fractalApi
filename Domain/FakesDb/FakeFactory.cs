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
            

            var fake = A.Fake<IGridRepository>();
            A.CallTo(() => fake.Get("main")).Returns(CreateMainGrid());
            A.CallTo(() => fake.Get("subgrid")).Returns(CreateSubGrid());
            return fake;
        }

        public static IArticleRepository GetArticleRepository()
        {
            return A.Fake<IArticleRepository>();
        }

        public static INoteRepository GetNoteRepository()
        {
            var fake = A.Fake<INoteRepository>();

            A.CallTo(() => fake.Create(new Item()))
             .Returns(new Item(){id=5});

            return fake;
        }

        private static Grid CreateMainGrid()
        {
            var note = new Item()
            {
                id = 1,
                type = "note",
                analogy = new int[] { 1, 2 },
                sub = new int[1] { 1 },
                sup = new int[1] { 1 },
                title = "Fractal",
                text = "Добро пожаловать!"
            };

            var gridItem = new Item()
            {
                id = 2,
                type = "gridItem",
                analogy = new int[] { 1 },
                sub = new int[] { },
                sup = new int[] { },
                slug = "subgrid",
                title = "Нужно больше листов",
                text = "Да, это фейк, но он уже должен быть!"
            };

            return new Grid()
            {
                Slug = "main",
                Title = "Fractal",
                Width = 4,
                FixedWidth = true,
                Items = new Item[4][]
                {
                    new Item[]{note, note},
                    new Item[]{gridItem, gridItem},
                    new Item[]{gridItem, gridItem},
                    new Item[]{note, note}
                },

                PageTitle = "Главная - Fractal",
                PageDescription = "",
                PageKeywords = "",
            };
        }

         private static Grid CreateSubGrid()
        {
            var note = new Item()
            {
                id = 1,
                type = "note",
                analogy = new int[] { 1, 2 },
                sub = new int[1] { 1 },
                sup = new int[1] { 1 },
                title = "Заметка",
                text = "Эта заметка подлиста."
            };

            var gridItem = new Item()
            {
                id = 2,
                type = "gridItem",
                analogy = new int[] { 1 },
                sub = new int[] { },
                sup = new int[] { },
                slug = "main",
                title = "Fractal",
                text = "Вернуться на главный лист"
            };

            return new Grid()
            {
                Slug = "subgrid",
                Title = "Fractal > subgrid",
                Width = 4,
                FixedWidth = true,
                Items = new Item[4][]
                {
                    new Item[]{note},
                    new Item[]{gridItem},
                    new Item[]{gridItem},
                    new Item[]{note}
                },

                PageTitle = "Subgrid - Fractal",
                PageDescription = "",
                PageKeywords = "",
            };
        }
    }
}
