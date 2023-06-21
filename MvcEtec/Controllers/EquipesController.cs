using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections.Generic;
using MvcEtec.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace MvcEtec.Controllers
{
    public class EquipesController : Controller
    {
        public string uriBase = "http://glennancy.somee.com/ApiEtec/Equipes/";


        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                string uriComplementar = "GetAll";
                HttpClient httpClient = new HttpClient();
                string token =  HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<EquipeViewModel> listaEquipes = await Task.Run(() => 
                        JsonConvert.DeserializeObject<List<EquipeViewModel>>(serialized));

                    return View(listaEquipes);
                }
                else
                    throw new System.Exception(serialized);

            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(EquipeViewModel e)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token =  HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(e));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if ( response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Equipe {0}, salva comsucesso!!!", e.NomeEquipe);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized);

            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Create");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> DetailsAsync(int? id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token =  HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    EquipeViewModel e = await Task.Run(() => 
                        JsonConvert.DeserializeObject<EquipeViewModel>(serialized));
                    return View(e);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

       
        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token =  HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

               if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Equipe Id {0} removida com sucesso!!!", id);
                    return RedirectToAction("Index");
                }
                else
                    throw new System.Exception(serialized); 
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}