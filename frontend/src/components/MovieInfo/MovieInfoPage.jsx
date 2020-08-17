import React, { Component } from "react";
import "./MovieInfoPage.scss";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import axios from "axios";
import Movie from "../Movie/Movie";
import Comments from "./Comments";

var movieID = "";
var token = "";

const MovieInfoPage = observer(
  class MovieInfoPage extends Component {
    movie = { FileID: "1180C2F6-A482-49F1-9628-5CA3D7EA6A3B" };
    lists = [];
    list = "";
    componentDidMount() {
      axios
        .get("https://localhost:44336/api/Movie/" + this.props.match.params.id)
        .then((response) => (this.movie = response.data));
      if (this.props.token) {
        this.getLists();
      }
    }

    handleInputChange = (event) => {
      const target = event.target;
      this.list = target.value;
    };

    postComment() {
      if (token !== "prazno") {
        let comm = document.getElementById("commentToPost").value;
        let rating = document.getElementById("rating").value;

        axios
          .post(
            "https://localhost:44336/api/Review/" + movieID,
            { NumberOfStars: rating, Comment: comm },
            { headers: { Authorization: `Bearer ${token}` } }
          )
          .then((response) => {
            console.log(response);
          });
      }
    }

    getLists = async () => {
      await axios
        .get("https://localhost:44336/api/MovieLists/Lists", {
          headers: { Authorization: `Bearer ${this.props.token}` },
        })
        .then((response) => {
          this.lists = response.data.m_Item2;
        });
    };

    addMovieToList = () => {
      let movie = {
        ListName: this.list,
        MovieID: this.movie.MovieID,
      };
      axios.post("https://localhost:44336/api/MovieLists", movie, {
        headers: { Authorization: `Bearer ${this.props.token}` },
      });
    };

    render() {
      let moviesProp = [];
      if (this.movie.length !== 0) {
        moviesProp = this.movie;
      }
      let listsToSelect = [];
      if (this.lists.length !== 0) {
        listsToSelect = this.lists.map((list) => {
          return (
            <option key={list.ListName + "movieInfoPage"} value={list.ListName}>
              {list.ListName}
            </option>
          );
        });
      }
      token = this.props.token ? this.props.token : "prazno";
      movieID = this.movie.MovieID;
      return (
        <div>
          <div className="base-container">
            <div className="image">
              <Movie key={this.movie.MovieID} movie={moviesProp} />
            </div>
          </div>
          {this.props.token ? (
            <div className="forma">
              <p>Add to my list</p>
              <select onChange={this.handleInputChange} value={this.list}>
                <option onChange={this.handleInputChange}>*</option>
                {listsToSelect}
              </select>
              <button className="btn" onClick={this.addMovieToList}>Add</button>
            </div>
          ) : null}
          <div className="info">
            <div>Title: {this.movie.Title}</div>
            <div>Year of production: {this.movie.YearOfProduction}</div>
            <div>Country of origin: {this.movie.CountryOfOrigin}</div>
            <div>Duration: {this.movie.Duration} minutes</div>
            <div>Rating: {this.movie.Rating} / 10</div>
            <div></div>
            <div></div>
          </div>
          <div className="error">
            {token === "prazno" ? "Login to comment" : ""}
          </div>
          <div className="color">Comments</div>

          <div id="comment_form">
            <div>
              <textarea
                placeholder="Comment here"
                id="commentToPost"
                name="Text1"
                className="input"
                maxLength="999"
                rows="6"
              ></textarea>
            </div>
          </div>
          <div className="starRow">
            RATE (1-10 ):
            <input id="rating" placeholder="1-10"></input>
            <button
              type="button"
              className="postBtn"
              onClick={this.postComment.bind(this.movie.MovieID)}
            >
              Post
            </button>
          </div>
          <div className="container">
            <div className="dialogbox">
              <div className="body">
                <span className="tip tip-up"></span>
                <div className="message">
                  <span>
                    <Comments movieID={this.props.match.params.id} />
                  </span>
                </div>
              </div>
            </div>
          </div>
        </div>
      );
    }
  }
);

decorate(MovieInfoPage, {
  movie: observable,
  lists: observable,
  list: observable,
});

export default MovieInfoPage;
