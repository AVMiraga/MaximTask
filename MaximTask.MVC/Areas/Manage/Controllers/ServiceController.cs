using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MaximTask.Business.Services.Interfaces;
using MaximTask.Business.ViewModel;
using MaximTask.Core.Entities;
using MaximTask.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MaximTask.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _service;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ServiceController(IServiceService service, IMapper mapper, AppDbContext context)
        {
            _service = service;
            _mapper = mapper;
            _context = context;
        }

        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> Index()
        {

            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            if(roles.Contains("Admin"))
            {
                IEnumerable<Service> entities = _context.Services;
                return View(entities);
            }
            else
            {
                var entities = await _service.GetAllAsync();
                return View(entities);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateServiceVm vm)
        {
            CreateServiceVmValidator validator = new CreateServiceVmValidator();
            ValidationResult result = await validator.ValidateAsync(vm);

            if(!result.IsValid)
            {
                ModelState.Clear();

                result.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View();
            }

            await _service.CreateAsync(vm);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var entity = await _service.GetById(id);

            if(entity is null)
            {
                return RedirectToAction("Index");
            }

            UpdateServiceVm vm = _mapper.Map<UpdateServiceVm>(entity);

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateServiceVm vm)
        {
            UpdateServiceVmValidator validator = new UpdateServiceVmValidator();
            ValidationResult result = await validator.ValidateAsync(vm);

            if (!result.IsValid)
            {
                ModelState.Clear();

                result.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View();
            }

            await _service.UpdateAsync(vm);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id <= 0)
            {
                return RedirectToAction("Index");
            }

            await _service.DeleteAsync(id); 
            
            return RedirectToAction("Index");
        }
    }
}
