using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicLib.BLL.DTO;
using MusicLib.BLL.Interfaces;
using MusicLib.BLL.Infrastructure;
using MusicLib.Models;

namespace MusicLib.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongService songService;
        private readonly IGenreService genreService;
        private readonly IArtistService artistService;
        private readonly IAudioService audioService;
        public SongsController(ISongService songserv, 
            IGenreService genreserv,
            IArtistService artistserv,
            IAudioService audioserv )
        {
            songService = songserv;
            genreService = genreserv;
            artistService = artistserv;
            audioService = audioserv;
        }

        // GET: Songs
        // Sorting + filtering + pagination
        public async Task<IActionResult> Index(
            SortState sortOrder = SortState.SongTitleAsc, 
            int artist = 0, 
            int genre = 0, 
            int page = 1)
        {
            int pageSize = 8;

            //получаем сортированный список песен
            var songs = await songService.GetSortedItemsAsync(sortOrder.ToString());
            var songsList = songs.ToList();

            //фильтрация
            if (artist != null && artist!=0)  {
                songsList = songsList.Where(p => p.ArtistId == artist).ToList();
            }

            if (genre != null && genre != 0)  {
                songsList = songsList.Where(p => p.GenreId == genre).ToList();
            }

            var genres = (await genreService.GetGenres()).ToList();
            var artists = (await artistService.GetArtists()).ToList();

            // устанавливаем начальный элемент, который позволит выбрать всех
            genres.Insert(0, new GenreDTO { Title = "All", Id = 0 });
            artists.Insert(0, new ArtistDTO { Name = "All", Id = 0 });            

            // пагинация
            var count = songsList.Count();
            var items = songsList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            IndexViewModel viewModel = new IndexViewModel
            (
                items,
                new PageViewModel(count, page, pageSize),
                new FilterViewModel(genres, genre, artists, artist),
                new SortViewModel(sortOrder)                
            );

            return View(viewModel);
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                SongDTO song = await songService.GetSong((int)id);
                return View(song);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: Songs/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ListGenres = new SelectList(await genreService.GetGenres(), "Id", "Title");
            ViewBag.ListArtists = new SelectList(await artistService.GetArtists(), "Id", "Name");
            ViewBag.ListAudios = new SelectList(await audioService.GetAudios(), "Id", "FileName");
            return View();
        }

        // POST: Songs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SongDTO song)
        {
            if (ModelState.IsValid)
            {
                await songService.CreateSong(song);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ListGenres = new SelectList(await genreService.GetGenres(), "Id", "Title", song.GenreId);
            ViewBag.ListArtists = new SelectList(await artistService.GetArtists(), "Id", "Name", song.ArtistId);
            ViewBag.ListAudios = new SelectList(await audioService.GetAudios(), "Id", "FileName", song.AudioId);

            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                SongDTO song = await songService.GetSong((int)id);
                ViewBag.ListGenres = new SelectList(await genreService.GetGenres(), "Id", "Title", song.GenreId);
                ViewBag.ListArtists = new SelectList(await artistService.GetArtists(), "Id", "Name", song.ArtistId);
                ViewBag.ListAudios = new SelectList(await audioService.GetAudios(), "Id", "FileName", song.AudioId);
                return View(song);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }


        // POST: Songs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SongDTO song)
        {
            if (ModelState.IsValid)
            {
                await songService.UpdateSong(song);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ListGenres = new SelectList(await genreService.GetGenres(), "Id", "Title", song.GenreId);
            ViewBag.ListArtists = new SelectList(await artistService.GetArtists(), "Id", "Name", song.ArtistId);
            ViewBag.ListAudios = new SelectList(await audioService.GetAudios(), "Id", "FileName", song.AudioId);
            return View(song);
        }


        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                SongDTO song = await songService.GetSong((int)id);
                return View(song);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await songService.DeleteSong(id);
            return RedirectToAction(nameof(Index));
        }

    }
}