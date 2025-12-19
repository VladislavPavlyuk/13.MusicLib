using MusicLib.DAL.Entities;

namespace MusicLib.DAL.EF
{
    public static class DbInitializer
    {
        public static void Initialize(MusicLibContext context)
        {
            if (context.Database.EnsureCreated())
            {
                SeedData(context);
            }
        }

        public static void SeedData(MusicLibContext context)
        {
            if (context.Roles.Any())
                return;

            context.Roles.Add(new Role { Title = "Admin" });
            context.Roles.Add(new Role { Title = "User" });
            context.Roles.Add(new Role { Title = "Candidate" });

            context.Users.Add(new User
            {
                Email = "admin@admin.com",
                Password = "63F66566834843057ECD47890F10987FBD0D2022BB2A8ED84ED04890B9644E1C",
                Salt = "073B6AA3BED5420579D70404FD470461",
                RoleId = 1
            });

            context.Genres.Add(new Genre { Title = "Rock" });
            context.Genres.Add(new Genre { Title = "Pop" });
            context.Genres.Add(new Genre { Title = "Rap" });
            context.Genres.Add(new Genre { Title = "Jazz" });
            context.Genres.Add(new Genre { Title = "Classic" });
            context.Genres.Add(new Genre { Title = "Metal" });
            context.Genres.Add(new Genre { Title = "Blues" });
            context.Genres.Add(new Genre { Title = "Country" });
            context.Genres.Add(new Genre { Title = "Electronic" });
            context.Genres.Add(new Genre { Title = "Folk" });
            context.Genres.Add(new Genre { Title = "Indie" });
            context.Genres.Add(new Genre { Title = "Reggae" });
            context.Genres.Add(new Genre { Title = "Latin" });
            context.Genres.Add(new Genre { Title = "Punk" });
            context.Genres.Add(new Genre { Title = "Soul" });
            context.Genres.Add(new Genre { Title = "R&B" });
            context.Genres.Add(new Genre { Title = "Gospel" });
            context.Genres.Add(new Genre { Title = "New Age" });
            context.Genres.Add(new Genre { Title = "World" });
            context.Genres.Add(new Genre { Title = "Experimental" });
            context.Genres.Add(new Genre { Title = "Easy Listening" });
            context.Genres.Add(new Genre { Title = "Soundtrack" });
            context.Genres.Add(new Genre { Title = "Comedy" });
            context.Genres.Add(new Genre { Title = "Children's" });
            context.Genres.Add(new Genre { Title = "Holiday" });
            context.Genres.Add(new Genre { Title = "Other" });

            context.Artists.Add(new Artist { Name = "The Beatles" });
            context.Artists.Add(new Artist { Name = "Elvis Presley", BirthDate = "01/08/1935" });
            context.Artists.Add(new Artist { Name = "Michael Jackson", BirthDate = "08/29/1958" });
            context.Artists.Add(new Artist { Name = "Elton John", BirthDate = "03/25/1947" });
            context.Artists.Add(new Artist { Name = "Madonna", BirthDate = "08/16/1958" });
            context.Artists.Add(new Artist { Name = "Led Zeppelin" });
            context.Artists.Add(new Artist { Name = "Pink Floyd" });
            context.Artists.Add(new Artist { Name = "Queen" });
            context.Artists.Add(new Artist { Name = "The Rolling Stones" });
            context.Artists.Add(new Artist { Name = "Bob Dylan", BirthDate = "05/24/1941" });
            context.Artists.Add(new Artist { Name = "David Bowie", BirthDate = "01/08/1947" });
            context.Artists.Add(new Artist { Name = "Bruce Springsteen", BirthDate = "09/23/1949" });
            context.Artists.Add(new Artist { Name = "Prince", BirthDate = "06/07/1958" });
            context.Artists.Add(new Artist { Name = "The Who" });
            context.Artists.Add(new Artist { Name = "Stevie Wonder", BirthDate = "05/13/1950" });
            context.Artists.Add(new Artist { Name = "Bob Marley", BirthDate = "02/06/1945" });
            context.Artists.Add(new Artist { Name = "James Brown", BirthDate = "05/03/1933" });
            context.Artists.Add(new Artist { Name = "U2" });
            context.Artists.Add(new Artist { Name = "The Doors" });
            context.Artists.Add(new Artist { Name = "Aretha Franklin", BirthDate = "03/25/1942" });
            context.Artists.Add(new Artist { Name = "Nirvana" });
            context.Artists.Add(new Artist { Name = "Jimi Hendrix", BirthDate = "11/27/1942" });

            context.SaveChanges();

            context.Songs.Add(new Song { Title = "Bohemian Rhapsody", Release = "1975", GenreId = 1, ArtistId = 7, YoutubeLink = "https://www.youtube.com/watch?v=fJ9rUzIMcZQ" });
            context.Songs.Add(new Song { Title = "Imagine", Release = "1971", GenreId = 1, ArtistId = 4, YoutubeLink = "https://www.youtube.com/watch?v=DVg2EJvvlF8" });
            context.Songs.Add(new Song { Title = "Hotel California", Release = "1977", GenreId = 1, ArtistId = 20, YoutubeLink = "https://www.youtube.com/watch?v=EqPtz5qN7HM" });
            context.Songs.Add(new Song { Title = "Stairway to Heaven", Release = "1971", GenreId = 1, ArtistId = 6, YoutubeLink = "https://www.youtube.com/watch?v=QkF3oxziUI4" });
            context.Songs.Add(new Song { Title = "Like a Rolling Stone", Release = "1965", GenreId = 1, ArtistId = 10, YoutubeLink = "https://www.youtube.com/watch?v=JGfXiIXTpU0" });
            context.Songs.Add(new Song { Title = "Hey Jude", Release = "1968", GenreId = 1, ArtistId = 1, YoutubeLink = "https://www.youtube.com/watch?v=A_MjCqQoLLA" });
            context.Songs.Add(new Song { Title = "Smells Like Teen Spirit", Release = "1991", GenreId = 1, ArtistId = 19, YoutubeLink = "https://www.youtube.com/watch?v=hTWKbfoikeg" });
            context.Songs.Add(new Song { Title = "What's Going On", Release = "1971", GenreId = 1, ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=H-kA3UtBj4M" });
            context.Songs.Add(new Song { Title = "Born to Run", Release = "1975", GenreId = 1, ArtistId = 11, YoutubeLink = "https://www.youtube.com/watch?v=IxuThNgl3YA" });
            context.Songs.Add(new Song { Title = "I Want to Hold Your Hand", Release = "1963", GenreId = 1, ArtistId = 1, YoutubeLink = "https://www.youtube.com/watch?v=jenWdylTtzs" });
            context.Songs.Add(new Song { Title = "Purple Haze", Release = "1967", GenreId = 1, ArtistId = 21, YoutubeLink = "https://www.youtube.com/watch?v=ccvHJU5O4ZQ" });
            context.Songs.Add(new Song { Title = "A Change Is Gonna Come", Release = "1964", GenreId = 1, ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=wEBlaMOmKV4" });
            context.Songs.Add(new Song { Title = "Lose Yourself", Release = "2002", GenreId = 1, ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=_Yhyp-_hX2s" });
            context.Songs.Add(new Song { Title = "Let It Be", Release = "1970", GenreId = 1, ArtistId = 1, YoutubeLink = "https://www.youtube.com/watch?v=QDYfEBY9NM4" });
            context.Songs.Add(new Song { Title = "I Walk the Line", Release = "1956", GenreId = 1, ArtistId = 8, YoutubeLink = "https://www.youtube.com/watch?v=4f0p5KqdUfM" });
            context.Songs.Add(new Song { Title = "Billie Jean", Release = "1982", GenreId = 1, ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=Zi_XLOBDo_Y" });
            context.Songs.Add(new Song { Title = "Thunder Road", Release = "1975", GenreId = 1, ArtistId = 11, YoutubeLink = "https://www.youtube.com/watch?v=JZAM3N4bZgY" });
            context.Songs.Add(new Song { Title = "Light My Fire", Release = "1967", GenreId = 1, ArtistId = 20, YoutubeLink = "https://www.youtube.com/watch?v=flsBdWx5l4w" });
            context.Songs.Add(new Song { Title = "Born in the U.S.A.", Release = "1984", GenreId = 1, ArtistId = 11, YoutubeLink = "https://www.youtube.com/watch?v=lZD4ezDbbu4" });
            context.Songs.Add(new Song { GenreId = 2, Title = "Billie Jean", Release = "1982", ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=Zi_XLOBDo_Y" });
            context.Songs.Add(new Song { GenreId = 2, Title = "Thriller", Release = "1982", ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=sOnqjkJTMaA" });
            context.Songs.Add(new Song { GenreId = 2, Title = "Beat It", Release = "1983", ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=oRdxUFDoQe0" });
            context.Songs.Add(new Song { GenreId = 2, Title = "Smooth Criminal", Release = "1987", ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=h_D3VFfhvs4" });
            context.Songs.Add(new Song { GenreId = 2, Title = "Black or White", Release = "1991", ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=F2AitTPI5U0" });
            context.Songs.Add(new Song { GenreId = 2, Title = "The Way You Make Me Feel", Release = "1987", ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=HzZ_urpj4As" });
            context.Songs.Add(new Song { GenreId = 3, Title = "Lose Yourself", Release = "2002", ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=_Yhyp-_hX2s" });
            context.Songs.Add(new Song { GenreId = 3, Title = "Without Me", Release = "2002", ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=YVkUvmDQ3HY" });
            context.Songs.Add(new Song { GenreId = 3, Title = "The Real Slim Shady", Release = "2000", ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=eJO5HU_7_1w" });
            context.Songs.Add(new Song { GenreId = 3, Title = "Stan", Release = "2000", ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=gOMhN-hfMtY" });
            context.Songs.Add(new Song { GenreId = 3, Title = "Mockingbird", Release = "2004", ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=S9bCLPwzSC0" });
            context.Songs.Add(new Song { GenreId = 3, Title = "Rap God", Release = "2013", ArtistId = 3, YoutubeLink = "https://www.youtube.com/watch?v=XbGs_qK2PQA" });
            context.Songs.Add(new Song { GenreId = 4, Title = "What's Going On", Release = "1971", ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=H-kA3UtBj4M" });
            context.Songs.Add(new Song { GenreId = 4, Title = "Let's Get It On", Release = "1973", ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=x6QZn9xiuOE" });
            context.Songs.Add(new Song { GenreId = 4, Title = "Sexual Healing", Release = "1982", ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=rjlSiASsUIs" });
            context.Songs.Add(new Song { GenreId = 4, Title = "Mercy Mercy Me", Release = "1971", ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=U4WiyxXpyZc" });
            context.Songs.Add(new Song { GenreId = 4, Title = "Inner City Blues", Release = "1971", ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=57Ykv1D0qEE" });
            context.Songs.Add(new Song { GenreId = 4, Title = "I Heard It Through the Grapevine", Release = "1968", ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=cJZp2XzmeGc" });
            context.Songs.Add(new Song { GenreId = 5, Title = "What a Wonderful World", Release = "1967", ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=A3yCcXgbKrE" });
            context.Songs.Add(new Song { GenreId = 5, Title = "La Vie en Rose", Release = "1947", ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=0NUX4tW5pps" });
            context.Songs.Add(new Song { GenreId = 5, Title = "Autumn Leaves", Release = "1945", ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=9G4jnaznUoQ" });
            context.Songs.Add(new Song { GenreId = 5, Title = "Non, Je Ne Regrette Rien", Release = "1960", ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=Q3Kvu6Kgp88" });
            context.Songs.Add(new Song { GenreId = 5, Title = "Sous le Ciel de Paris", Release = "1951", ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=3v6J6J8v7D0" });
            context.Songs.Add(new Song { GenreId = 5, Title = "Milord", Release = "1959", ArtistId = 16, YoutubeLink = "https://www.youtube.com/watch?v=3v6J6J8v7D0" });
            context.Songs.Add(new Song { GenreId = 6, Title = "Stairway to Heaven", Release = "1971", ArtistId = 6, YoutubeLink = "https://www.youtube.com/watch?v=QkF3oxziUI4" });
            context.Songs.Add(new Song { GenreId = 6, Title = "Whole Lotta Love", Release = "1969", ArtistId = 6, YoutubeLink = "https://www.youtube.com/watch?v=HQmmM_qwG4k" });
            context.Songs.Add(new Song { GenreId = 7, Title = "Purple Haze", Release = "1967", ArtistId = 21, YoutubeLink = "https://www.youtube.com/watch?v=ccvHJU5O4ZQ" });
            context.Songs.Add(new Song { GenreId = 7, Title = "Voodoo Child", Release = "1968", ArtistId = 21, YoutubeLink = "https://www.youtube.com/watch?v=IZBlqcbpmxY" });
            context.Songs.Add(new Song { GenreId = 8, Title = "I Walk the Line", Release = "1956", ArtistId = 8, YoutubeLink = "https://www.youtube.com/watch?v=4f0p5KqdUfM" });
            context.Songs.Add(new Song { GenreId = 8, Title = "Ring of Fire", Release = "1963", ArtistId = 8, YoutubeLink = "https://www.youtube.com/watch?v=It7107ELQvY" });
            context.Songs.Add(new Song { GenreId = 8, Title = "Folsom Prison Blues", Release = "1955", ArtistId = 8, YoutubeLink = "https://www.youtube.com/watch?v=xbJQT5JbSuA" });
            context.Songs.Add(new Song { GenreId = 9, ArtistId = 1, Title = "Hey Jude", Release = "1968", YoutubeLink = "https://www.youtube.com/watch?v=A_MjCqQoLLA" });
            context.Songs.Add(new Song { GenreId = 9, ArtistId = 1, Title = "Let It Be", Release = "1970", YoutubeLink = "https://www.youtube.com/watch?v=QDYfEBY9NM4" });
            context.Songs.Add(new Song { GenreId = 9, ArtistId = 1, Title = "I Want to Hold Your Hand", Release = "1963", YoutubeLink = "https://www.youtube.com/watch?v=jenWdylTtzs" });
            context.Songs.Add(new Song { GenreId = 9, ArtistId = 1, Title = "Yesterday", Release = "1965", YoutubeLink = "https://www.youtube.com/watch?v=ONXGJ3r52Eg" });
            context.Songs.Add(new Song { GenreId = 9, ArtistId = 1, Title = "Come Together", Release = "1969", YoutubeLink = "https://www.youtube.com/watch?v=45cYwDMibGo" });
            context.Songs.Add(new Song { GenreId = 10, ArtistId = 1, Title = "Yesterday", Release = "1965", YoutubeLink = "https://www.youtube.com/watch?v=ONXGJ3r52Eg" });
            context.Songs.Add(new Song { GenreId = 11, ArtistId = 1, Title = "Come Together", Release = "1969", YoutubeLink = "https://www.youtube.com/watch?v=45cYwDMibGo" });
            context.Songs.Add(new Song { GenreId = 11, ArtistId = 1, Title = "Help!", Release = "1965", YoutubeLink = "https://www.youtube.com/watch?v=2Q_ZzBGPdqE" });
            context.Songs.Add(new Song { GenreId = 11, ArtistId = 1, Title = "A Hard Day's Night", Release = "1964", YoutubeLink = "https://www.youtube.com/watch?v=Yjyj8qnqkYI" });
            context.Songs.Add(new Song { GenreId = 11, ArtistId = 1, Title = "Can't Buy Me Love", Release = "1964", YoutubeLink = "https://www.youtube.com/watch?v=3Z2vU8M6CYI" });
            context.Songs.Add(new Song { GenreId = 11, ArtistId = 1, Title = "Twist and Shout", Release = "1963", YoutubeLink = "https://www.youtube.com/watch?v=Zfc7G9p4bso" });
            context.Songs.Add(new Song { GenreId = 13, ArtistId = 2, Title = "Hound Dog", Release = "1956", YoutubeLink = "https://www.youtube.com/watch?v=MMmljYkdr-w" });
            context.Songs.Add(new Song { GenreId = 13, ArtistId = 2, Title = "Jailhouse Rock", Release = "1957", YoutubeLink = "https://www.youtube.com/watch?v=gj0Rz-uP4Mk" });
            context.Songs.Add(new Song { GenreId = 13, ArtistId = 2, Title = "Can't Help Falling in Love", Release = "1961", YoutubeLink = "https://www.youtube.com/watch?v=vGJTaP6anOU" });
            context.Songs.Add(new Song { GenreId = 13, ArtistId = 2, Title = "Suspicious Minds", Release = "1969", YoutubeLink = "https://www.youtube.com/watch?v=e-NDXtDUcGQ" });
            context.Songs.Add(new Song { GenreId = 13, ArtistId = 2, Title = "Love Me Tender", Release = "1956", YoutubeLink = "https://www.youtube.com/watch?v=HZBUb0ElnNY" });
            context.Songs.Add(new Song { GenreId = 14, ArtistId = 3, Title = "Billie Jean", Release = "1982", YoutubeLink = "https://www.youtube.com/watch?v=Zi_XLOBDo_Y" });
            context.Songs.Add(new Song { GenreId = 14, ArtistId = 2, Title = "Thriller", Release = "1982", YoutubeLink = "https://www.youtube.com/watch?v=sOnqjkJTMaA" });
            context.Songs.Add(new Song { GenreId = 14, ArtistId = 3, Title = "Beat It", Release = "1983", YoutubeLink = "https://www.youtube.com/watch?v=oRdxUFDoQe0" });
            context.Songs.Add(new Song { GenreId = 14, ArtistId = 3, Title = "Smooth Criminal", Release = "1987", YoutubeLink = "https://www.youtube.com/watch?v=h_D3VFfhvs4" });
            context.Songs.Add(new Song { GenreId = 14, ArtistId = 3, Title = "Black or White", Release = "1991", YoutubeLink = "https://www.youtube.com/watch?v=F2AitTPI5U0" });
            context.Songs.Add(new Song { GenreId = 15, ArtistId = 3, Title = "The Way You Make Me Feel", Release = "1987", YoutubeLink = "https://www.youtube.com/watch?v=HzZ_urpj4As" });
            context.Songs.Add(new Song { GenreId = 15, ArtistId = 3, Title = "Bad", Release = "1987", YoutubeLink = "https://www.youtube.com/watch?v=dsUXAEzaC3Q" });

            context.SaveChanges();
        }
    }
}
