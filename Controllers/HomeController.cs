using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using hackathon.Models;
using System.Reflection.Emit;
using Newtonsoft.Json;
using LibGit2Sharp;

namespace hackathon.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            var response = new
            {msg="Welcome to Linux Accelerator" };
            return Ok(response);
        }
    }
    [Route("/GetMetadata")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        [HttpGet]
        public IActionResult get(string repoOrg=".", string repoName=".")
        {
            string path = String.Concat("./project/", String.Concat(repoOrg,String.Concat("/",repoName)));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory("path");
                string repo = String.Concat("https://github.com/", String.Concat(repoOrg, String.Concat("/", repoName)));
                repo = String.Concat(repo, ".git");
                try { 
                    object p = LibGit2Sharp.Repository.Clone(repo, path); }
                catch{}

            }
            string filePath = String.Concat(path, "/metadata.json");
            string json = "file not found..";
            try { 
                json = System.IO.File.ReadAllText(filePath);
                JsonConvert.DeserializeObject<MetadataModel>(json);
            }
            catch { }
            
            return Ok(json);
        }
    }
}
