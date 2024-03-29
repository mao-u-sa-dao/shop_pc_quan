﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Website_Laptop.Models;
using System.Linq;
using Website_Laptop.Models.Authentication;

namespace Website_Laptop.Areas.Admin.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminProductApiController : ControllerBase
    {
        private readonly QliBanPcContext db = new QliBanPcContext();
        [Authentication]
        [HttpGet("{id}")]
        public IActionResult GetProductById(string id)
        {
            var sanPham = db.PcDanhMucSps.SingleOrDefault(x => x.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return Ok(sanPham);
        }
        [Authentication]
        [HttpPost]
        public async Task<ActionResult<PcDanhMucSp>> AddProduct(PcDanhMucSp product)
        {
            if (ModelState.IsValid)
            {
                db.PcDanhMucSps.Add(product);
                await db.SaveChangesAsync();
                return CreatedAtAction("GetProduct", new { id = product.MaSp }, product);
            }
            return BadRequest(ModelState);
        }
        [Authentication]
        [HttpPut("{id}")]
        public async Task<ActionResult<PcDanhMucSp>> UpdateProductApi([FromBody] PcDanhMucSp product, string id)
        {
            var sanPham = db.PcDanhMucSps.SingleOrDefault(x => x.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }
            sanPham.MaSp = product.MaSp;
            sanPham.TenSp = product.TenSp;
            sanPham.CanNang = product.CanNang;
            sanPham.MaChatLieu = product.MaChatLieu;
            sanPham.MaQuocGiaSx = product.MaQuocGiaSx;
            sanPham.ThoiGianBaoHanh = product.ThoiGianBaoHanh;
            sanPham.GioiThieuSp = product.GioiThieuSp;
            sanPham.MaLoai = product.MaLoai;
            sanPham.AnhDaiDien = product.AnhDaiDien;
            sanPham.GiaSp = product.GiaSp;
            db.Update(sanPham);
            await db.SaveChangesAsync();
            return Ok();

        }
        [Authentication]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSp(string id)
        {

            var sanPham = db.PcDanhMucSps.FirstOrDefault(x => x.MaSp == id);
            var anhSanPham = db.PcAnhSps.Where(x => x.MaSp == id).ToList();
            if (anhSanPham != null && anhSanPham.Any())
            {
                db.RemoveRange(anhSanPham);
            }
            db.Remove(sanPham);
            await db.SaveChangesAsync();
            return Ok();
        }

    }
}
