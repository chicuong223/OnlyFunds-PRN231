using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using OnlyFundsAPI.BusinessObjects;
using OnlyFundsAPI.DataAccess.Interfaces;

namespace API.Controllers
{
    public class NotificationsController : ODataController
    {
        private readonly IRepoWrapper _repo;

        public NotificationsController(IRepoWrapper repo)
        {
            _repo = repo;
        }

        // GET: api/Notifications
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            return Ok(_repo.Notifications.GetList());
        }

        // GET: api/Notifications/5
        [EnableQuery]
        public async Task<SingleResult<Notification>> GetNotification(int key)
        {
            var result = _repo.Notifications.GetList().Where(e => e.NotificationID == key);

            return SingleResult.Create(result);
        }

        // PUT: api/Notifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Patch(int key, Delta<Notification> notification)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var existingNotifications = await _repo.Notifications.GetByID(key);
                if (existingNotifications == null) return BadRequest("Notification not found!");
                notification.Patch(existingNotifications);
                var result = await _repo.Notifications.Update(existingNotifications);
                return Updated(result);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException) return NotFound(ex.Message);
                throw new Exception(ex.Message);
            }

            return NoContent();
        }

        // POST: api/Notifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Notification>> Post(Notification notification)
        {
            if (NotificationExists(notification.NotificationID))
                return BadRequest("User has liked this comment");
            var result = _repo.Notifications.Create(notification);
            return Created(result);
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {
                await _repo.Notifications.Delete(id);
            }
            catch
            {
                if (!NotificationExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        private bool NotificationExists(int id)
        {
            return _repo.Notifications.GetByID(id) != null;
        }
    }
}
