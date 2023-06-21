using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections.Generic;
using MvcEtec.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace RpgMvc.Controllers
{
    public class AdicionarJogadorAEquipeController : Controller
    {
        public string uriBase = "http://glennancy.somee.com/ApiEtec/Jogadores/";

        [HttpPost]
        public async Task<ActionResult> CreateAsync(JogadorViewModel j)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token =  HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(j));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if ( response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Jogador {0}, salvo com sucesso!!!", j.Nome, serialized);
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
        public async Task<ActionResult> DetailsAsync(int? rm)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token =  HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + rm.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    JogadorViewModel j = await Task.Run(() => 
                        JsonConvert.DeserializeObject<JogadorViewModel>(serialized));
                    return View(j);
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
        public async Task<ActionResult> DeleteAsync(int rm)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token =  HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + rm.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

               if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Jogador Rm {0} removido com sucesso!!!", rm);
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