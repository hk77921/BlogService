using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogService.Model;
using System.Security.Cryptography;
using Newtonsoft.Json.Serialization;
using System.Runtime.InteropServices;
using System.Collections.Immutable;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogService.Controllers
{

  public static class Posts
  {
    public static readonly Model.Posts cmt;
    public static Dictionary<string, Model.Posts> _Postlst = new Dictionary<string, Model.Posts>();

    static Posts()
    {
      cmt = Factory.CreateComment();


      cmt.commentid = "0001";
      cmt.Title = "First Title";

      _Postlst.Add("0001", cmt);

      cmt = Factory.CreateComment();

      cmt.commentid = "0002";
      cmt.Title = "Second Title";


      _Postlst.Add("0002", cmt);

      cmt = Factory.CreateComment();

      // This is test to find watch reload
    }
  }




  public static class Factory
  {
    public static Model.Posts CreateComment()
    {
      return new Model.Posts();

    }

    public static Model.Posts CreateComment(string commentid, string title)
    {
      return new Model.Posts()
      {
        commentid = commentid,
        Title = title
      };

    }

  }


  [Route("api/[controller]")]
  [ApiController]
  public class PostsController : ControllerBase
  {

    [HttpGet]
    public IEnumerable<Model.Posts> Get()
    {
      List<Model.Posts> _comment = new List<Model.Posts>();

      foreach (var (key, value) in Posts._Postlst)
      {

        //  _comment.Add(new Comment { commentid = value.commentid, Title = value.Title });
        _comment.Add(Factory.CreateComment(value.commentid, value.Title));
      }

      return _comment.ToList();
    }

    // GET api/<CommentController>/5
    [HttpGet("{id}")]
    public Model.Posts Get(string id)
    {
      if (Posts._Postlst.ContainsKey(id))
        return Posts._Postlst[id];
      else
      {
        return new Model.Posts { commentid = "", Title = " " };
      }
    }



    // POST api/<CommentController>
    [HttpPost]
    public void Post([FromBody] Model.Posts comment)
    {


      string id = RandomNumberGenerator.GetInt32(100).ToString();

      Model.Posts _comment = Factory.CreateComment();

      _comment.Title = comment.Title;

      _comment.commentid = id;

      Posts._Postlst.Add(id, _comment);
      Response.StatusCode = 201;


    }




  }
}

