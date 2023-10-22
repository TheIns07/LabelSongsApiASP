using LabelSongsAPI.Data;
using LabelSongsAPI.Models.Relations;
using LabelSongsAPI.Models;

namespace LabelSongsAPI
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;

        }
        public void SeedDataContext()
        {
            if (!dataContext.SongComposers.Any())
            {
                var songComposers = new List<SongComposer>
                {
                    new SongComposer()
                    {
                        Song = new Song()
                        {
                            Name = "Machine Gun",
                            LaunchTime = DateTime.Now,
                            SongCategories = new List<SongCategory>()
                            {
                                new SongCategory{Category = new Category() { Name = "Alternative Electornic"} }
                            },
                            Reviews = new List<Review>()
                            {
                                new Review{ReviewTitle="AMAZING!", ReviewContent="Dark and amazing song" , Reviewer = new Reviewer{ Name="Taylor" }}
                            }
                        },
                        Composer = new Composer()
                        {
                            Name="Portishead",
                            Label = new Label
                            {
                                Name = "Universal"
                            }

                        }
                    },
                    new SongComposer()
                    {
                        Song = new Song()
                        {
                            Name = "School",
                            LaunchTime = DateTime.Now,
                            SongCategories = new List<SongCategory>()
                            {
                                new SongCategory{Category = new Category() { Name = "Grunge"} }
                            },
                            Reviews = new List<Review>()
                            {
                                new Review{ReviewTitle="AMAZING!", ReviewContent="Dark and amazing song", Reviewer = new Reviewer{ Name="Taylor" } }
                            }
                        },
                        Composer = new Composer()
                        {
                            Name="Nirvana",
                            Label = new Label
                            {
                                Name = "Universal"
                            }

                        }
                    }
                };
                dataContext.SongComposers.AddRange(songComposers);
                dataContext.SaveChanges();

            }
        }
    }
}
