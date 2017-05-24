using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreBusinessLayer;

namespace VideoStoreTest
{
    [TestFixture]
    public class VideoStoreTests
    {
        private VideoStore sut;
        private Movie TestMovie;
        [SetUp]
        public void Setup()
        {
            sut = new VideoStore();
            TestMovie = new Movie("Transporter", MovieGenre.Action);
        }
        [Test]
        public void TrueIfCanAddNewMovieToStore()
        {
            sut.AddMovie(TestMovie);
            var movie = sut.Movies.Where(x => x.Title == "Transporter" && x.Genre == MovieGenre.Action);
            Assert.IsTrue(movie != null);
        }
        [Test]
        public void MovieTitleCanNotBeNullOrEmpty()
        {
            TestMovie.Title = "";
            Assert.Throws<MovieTitleIsNullOrEmpty>(() =>
            {
                sut.AddMovie(TestMovie);
            });
        }
        [Test]
        public void ThrowExeptionIfAddedMoreThen3MoviesWithSameTitle()
        {
            sut.AddMovie(TestMovie);
            sut.AddMovie(TestMovie);
            sut.AddMovie(TestMovie);
            Assert.Throws<MovieTitleOverloadExeption>(() =>
            {
                sut.AddMovie(TestMovie);
            });
        }
    }
}
