
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services;

public class ProductsController : Controller
{
    private readonly IProductService _contextProduct;
    private readonly ICategoryService _contextCategory;

    public ProductsController(IProductService contextProduct, ICategoryService contextCategory)
    {
        _contextProduct = contextProduct;
        _contextCategory = contextCategory;
    }


    // GET: PRODUCTS
    public async Task<IActionResult> Index()
    {
        var myStoreContext = _contextProduct.GetProducts();
        return View(myStoreContext.ToList());
    }


    // GET: PRODUCTS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = _contextProduct.GetProductById((int)id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }


    // GET: PRODUCTS/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryId");
        return View();
    }


    // POST: PRODUCTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProductId,ProductName,CategoryId,UnitsInStock,UnitPrice")] Product product)
    {
        if (ModelState.IsValid)
        {
            _contextProduct.SaveProduct(product);

            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryId", product.CategoryId);
        return View(product);
    }


    // GET: PRODUCTS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = _contextProduct.GetProductById((int)id);
        if (product == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryId", product.CategoryId);
        return View(product);
    }


    // POST: PRODUCTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,CategoryId,UnitsInStock,UnitPrice")] Product product)
    {
        if (id != product.ProductId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _contextProduct.UpdateProduct(product);
            }
            catch (Exception)
            {
                if (!ProductExists(product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryId", product.CategoryId);
        return View(product);
    }


    // GET: PRODUCTS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = _contextProduct.GetProductById((int)id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }


    // POST: PRODUCTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = _contextProduct.GetProductById(id);
        if (product != null)
        {
            _contextProduct.DeleteProduct(product);
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        var tmp = _contextProduct.GetProductById(id);
        return (tmp != null) ? true : false;
    }

}
