using PupilRegister.Models.Entities;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using System;

namespace PupilRegister.DataContext
{
    public class PupilRegisterSeed
    {
        public static void Seed(ModelBuilder builder) 
        {
            var schools = Builder<School>.CreateListOfSize(20)
                .All()
                    .With(x => x.Name = Faker.Company.Name())
                .Build();

            var parents = Builder<Parent>.CreateListOfSize(30)
                .All()
                    .With(x => x.Email = Faker.Internet.Email())
                    .With(x => x.Name = Faker.Name.FullName())
                .Build();

            var pupils = Builder<Pupil>.CreateListOfSize(60)
                .All()
                    .With(x => x.Name = Faker.Name.FullName())
                    .With(x => x.ParentId = Faker.RandomNumber.Next(1, 30))
                    .With(x => x.SchoolId = Faker.RandomNumber.Next(1, 20))
                .Build();

            builder.Entity<School>().HasData(schools);
            builder.Entity<Parent>().HasData(parents);
            builder.Entity<Pupil>().HasData(pupils);
        }
    }
}
