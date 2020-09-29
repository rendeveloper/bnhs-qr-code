using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BnhsQrCode.Abstracts;
using BnhsQrCode.Model;
using BnhsQrCode.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;

namespace BnhsQrCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserImageFileService _userImageFileService;

        public UserController(IUserService userService, IUserImageFileService userImageFileService)
        {
            _userService = userService;
            _userImageFileService = userImageFileService;
        }
        // GET: api/User
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.Users.ToList();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = _userService.Users.Single(x => x.Id == id);
                return Ok(user);
            }
            catch
            {
                return Ok(false);
            }
        }

        // POST: api/User/Scan
        [HttpPost]
        [Route("Scan")]
        public IActionResult Scan([FromBody] ScanDTO scan)
        {
            try
            {
                var user = _userService.GetByTeacherId(scan.TeacherId);
                if (user == null) return Ok(false);

                return Ok(user);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO userDto)
        {
            try
            {
                _userService.BeginTransaction();

                var userModel = new User
                {
                    TeacherId = userDto.TeacherId.Trim(),
                    FirstName = userDto.FirstName.ToUpper().Trim(),
                    LastName = userDto.LastName.ToUpper().Trim(),
                    MiddleName = userDto.MiddleName.ToUpper().Trim(),
                    DateOfBirth = userDto.DateOfBirth,
                    Address = userDto.Address.Trim(),
                    HealthStatus = userDto.HealthStatus.Trim(),
                    Department = userDto.Department.Trim(),
                    Role = userDto.Role.Trim(),
                    Image = Upload(userDto.ImageDTO) ? userDto.ImageDTO.FileName : ""
                };

                await _userService.Save(userModel);
                await _userService.Commit();

                return Ok(userModel);
            }
            catch
            {
                // log exception here
                await _userService.Rollback();
                return Ok(false);
            }
            finally
            {
                _userService.CloseTransaction();
            }
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDTO userDto)
        {
            var userModel = await _userService.Users.SingleAsync(x => x.Id == userDto.Id);
            try
            {
                _userService.BeginTransaction();

                await _userImageFileService.DeleteFile(userModel.Image);

                userModel.TeacherId = userDto.TeacherId.Trim();
                userModel.FirstName = userDto.FirstName.ToUpper().Trim();
                userModel.LastName = userDto.LastName.ToUpper().Trim();
                userModel.MiddleName = userDto.MiddleName.ToUpper().Trim();
                userModel.DateOfBirth = userDto.DateOfBirth;
                userModel.Address = userDto.Address.Trim();
                userModel.HealthStatus = userDto.HealthStatus.Trim();
                userModel.Department = userDto.Department.Trim();
                userModel.Role = userDto.Role.Trim();
                userModel.Image = userDto.ImageDTO.DataBytes.Length == 0 ? userModel.Image : Upload(userDto.ImageDTO) ? userDto.ImageDTO.FileName : "";

                await _userService.Save(userModel);
                await _userService.Commit();
            }
            catch
            {
                // log exception here
                await _userService.Rollback();
                return Ok(false);
            }
            finally
            {
                _userService.CloseTransaction();
            }
            return Ok(userModel);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _userService.BeginTransaction();

                var user = _userService.Users.Single(x => x.Id == id);

                await _userService.Delete(user);
                await _userService.Commit();
            }
            catch
            {
                // log exception here
                await _userService.Rollback();
                return Ok(false);
            }
            finally
            {
                _userService.CloseTransaction();
            }
            return Ok(true);
        }

        // POST: api/User/Upload
        //[HttpPost]
        //[Route("Upload")]
        private bool Upload([FromBody] ImageDTO imageDto)
        {
          return _userService.Upload(imageDto);
        }
    }
}
