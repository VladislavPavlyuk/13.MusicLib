using System.IO;
using Microsoft.AspNetCore.Mvc;
using MusicLib.BLL.DTO;
using MusicLib.BLL.Interfaces;
using MusicLib.BLL.Infrastructure;


namespace MusicLib.Controllers
{
    public class AudiosController : Controller
    {
        private readonly IAudioService audioService;

        // IWebHostEnvironment предоставляет информацию об окружении, в котором запущено приложение
        IWebHostEnvironment _appEnvironment;

        public AudiosController(IAudioService serv, IWebHostEnvironment appEnvironment)
        {
            audioService = serv;
            _appEnvironment = appEnvironment;
        }

        // GET: Audios
        public async Task<IActionResult> Index()
        {
            return View(await audioService.GetAudios());
        }

        // GET: Audios/Details/5
        public async Task<IActionResult> Listen(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                AudioDTO audio = await audioService.GetAudio((int)id);

                return View(audio);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //
        // GET: /Audios/Create

        public IActionResult Create()
        {
            TempData["Message"] = "Model is empty...";
            return View();
        }

        /*
        // POST: Audios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AudioDTO audio)
        {
            if (ModelState.IsValid)
            {
                await audioService.CreateAudio(audio);
                return View("~/Views/Audios/Index.cshtml", await audioService.GetAudios());
            }
            return View(audio);
        }
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(10000000000)]
        public async Task<IActionResult> Create(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                TempData["Message"] = "File was uploaded successfully!";

                // Путь к папке Files
                string path = "/Files/" + uploadedFile.FileName; // имя файла

                // Сохраняем файл в папку Files в каталоге wwwroot
                // Для получения полного пути к каталогу wwwroot
                // применяется свойство WebRootPath объекта IWebHostEnvironment
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
                }
                AudioDTO file = new AudioDTO { FileName = uploadedFile.FileName, Path = path };

                //_context.Audio.Add(file);
                //_context.SaveChanges();

                await audioService.CreateAudio(file);

                return View("~/Views/Audios/Index.cshtml", await audioService.GetAudios());

            } else {
                TempData["Message"] = "File was NOT uploaded";

            }

            return RedirectToAction("Index");
        }

        // GET: Audios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                AudioDTO audio = await audioService.GetAudio((int)id);
                return View(audio);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Audios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AudioDTO audio)
        {
            if (ModelState.IsValid)
            {
                await audioService.UpdateAudio(audio);
                return View("~/Views/Audios/Index.cshtml", await audioService.GetAudios());
            }
            return View(audio);
        }

        // GET: Audios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                AudioDTO audio = await audioService.GetAudio((int)id);
                return View(audio);
            }
            catch (ValidationException ex)
            {                
                return NotFound(ex.Message);
            }
            catch(Exception ex) 
            {
                TempData["Message"] = ex.Message;

                return NotFound(ex.Message);
            }
        }

        // POST: Audios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                AudioDTO audio = await audioService.GetAudio((int)id);

                if (audio != null)
                    DeleteFile(audio.Path);
                else
                {
                    TempData["Message"] = "audio == null!";
                }

                await audioService.DeleteAudio(id);
            }
            catch(Exception ex)
            { 
                TempData["Message"] = ex.Message;
            }
            
            return View("~/Views/Audios/Index.cshtml", await audioService.GetAudios());
        }

        public void DeleteFile(string path)
        {           

            string filePath = _appEnvironment.WebRootPath + path;

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                TempData["Message"] = path + " was deleted successfully!";
            } else
            {
                TempData["Message"] = path + " was not found!";
            }
        }
    }
}
