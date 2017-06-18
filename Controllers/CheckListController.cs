using System.Collections.Generic;
using System.Linq;
using AzureApiApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzureApiApp.Controllers
{
    [Route("api/checklist")]
    public class CheckListController : Controller
    {
        private readonly ApiDbContext _context;

        public CheckListController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            var checkList = _context.CheckListItems.ToList();
            if (checkList != null)
            {
                return Ok(checkList);
            }
            return Json("No checklists exist in the database");
        }

        [HttpGet("{id}", Name="GetCheckList")]
        public IActionResult Get(int id)
        {
            var checkList = _context.CheckListItems.FirstOrDefault(c=>c.Id ==id);
            if (checkList == null)
            {
                return NotFound();
            }
            return new ObjectResult(checkList);
        }

        [HttpPost()]
        public IActionResult Create([FromBody] CheckList list)
        {
            if (list == null)
            {
                return BadRequest();
            }

            _context.CheckListItems.Add(list);
            _context.SaveChanges();
            return CreatedAtRoute("GetCheckList", new { id = list.Id }, list);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CheckList list)
        {
            if (list == null || list.Id != id)
            {
                return BadRequest();
            }

            var checkList = _context.CheckListItems.FirstOrDefault(c=>c.Id ==id);
            if(checkList == null)
            {
                return NotFound();
            }
            checkList.Name = list.Name;
            checkList.IsChecked = list.IsChecked;
            checkList.Category = list.Category;

            _context.CheckListItems.Update(checkList);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var checkList = _context.CheckListItems.First(c=>c.Id==id);
            if (checkList == null)
            {
                return NotFound();
            }

            _context.CheckListItems.Remove(checkList);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}