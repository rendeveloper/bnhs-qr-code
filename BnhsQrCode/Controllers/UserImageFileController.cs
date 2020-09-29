using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Abstracts;
using BnhsQrCode.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;

namespace BnhsQrCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImageFileController : ControllerBase
    {
        private readonly IUserImageFileService _userImageFileService;

        public UserImageFileController(IUserImageFileService userImageFileService)
        {
            _userImageFileService = userImageFileService;
        }
        // GET: api/UserImageFile
        [HttpGet]
        public IActionResult Get()
        {
            var imageFiles = _userImageFileService.UserImageFile.ToList();
            return Ok(imageFiles);
        }

        // GET: api/UserImageFile/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var imageFile = _userImageFileService.UserImageFile.Single(x => x.Id == id);
            return Ok(imageFile);
        }

        // POST: api/UserImageFile
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserImageFile userImageFile)
        {
            try
            {
                _userImageFileService.BeginTransaction();
                await _userImageFileService.Save(userImageFile);
                await _userImageFileService.Commit();
            }
            catch
            {
                // log exception here
                await _userImageFileService.Rollback();
                return Ok(false);
            }
            finally
            {
                _userImageFileService.CloseTransaction();
            }
            return Ok(true);
        }

        // PUT: api/UserImageFile/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserImageFile userImageFile)
        {
            var imageModel = await _userImageFileService.UserImageFile.SingleAsync(x => x.Id == userImageFile.Id);
            try
            {
                _userImageFileService.BeginTransaction();

                imageModel.UserProfileId = userImageFile.UserProfileId;
                imageModel.FileName = userImageFile.FileName;
                imageModel.OriginalFileName = userImageFile.OriginalFileName;
                imageModel.FileSize = userImageFile.FileSize;
                imageModel.UploadDate = userImageFile.UploadDate;

                await _userImageFileService.Save(imageModel);
                await _userImageFileService.Commit();
            }
            catch
            {
                // log exception here
                await _userImageFileService.Rollback();
                return Ok(false);
            }
            finally
            {
                _userImageFileService.CloseTransaction();
            }
            return Ok(imageModel);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _userImageFileService.BeginTransaction();

                var image = _userImageFileService.UserImageFile.Single(x => x.Id == id);

                await _userImageFileService.Delete(image);
                await _userImageFileService.Commit();
            }
            catch
            {
                // log exception here
                await _userImageFileService.Rollback();
                return Ok(false);
            }
            finally
            {
                _userImageFileService.CloseTransaction();
            }
            return Ok(true);
        }
    }
}
