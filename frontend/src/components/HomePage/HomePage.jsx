import React, { Component } from "react";
import axios from "axios";
import Movies from "../Movie/Movies";
import Cast from "../CastAndCrew/CC";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import "./HomePage.scss";

const HomePage = observer(
  class HomePage extends Component {
    movies = [];
    cast = [];
    pageNumber = 1;

    componentDidMount() {
      this.getMovies();
      this.getCast();
    }

    Next = () => {
      if ((this.pageNumber + 1) * 4 < this.movies.m_Item1 + 4) {
        this.pageNumber++;
        this.getMovies();
        this.getCast();
      }
    };

    Previous = () => {
      if (this.pageNumber !== 1) {
        this.pageNumber--;
        this.getMovies();
        this.getCast();
      }
    };

    getMovies = () => {
      axios
        .get(
          "https://localhost:44336/api/Movie?pageSize=4&pageNumber=" +
            this.pageNumber
        )
        .then((response) => {
          this.movies = response.data;
        });
    };

    getCast = () => {
      axios
        .get(
          "https://localhost:44336/api/CastAndCrew/SelectAsync?pageSize=4&pageNumber=" +
            this.pageNumber
        )
        .then((response) => {
          this.cast = response.data;
        });
    };

    render() {
      let moviesProp = [];
      let castProp = [];
      if (this.movies.length !== 0) {
        moviesProp = this.movies.m_Item2;
      }
      if (this.cast.length !== 0) {
        castProp = this.cast.m_Item2;
      }
      return (
        <div>
          <p>
            <img src={require("./c.png")} className="center" alt="" />
          </p>
          <h1 className="centerd">Welcome...</h1>
          <h3 className="centerd1">
            You've found the most accurate source for TV and film. Our
            information comes from fans like you, so create a free account and
            help your favorite shows and movies.
          </h3>
          <h2 className="title">Featured Movies</h2>
          <div className="spacer"></div>
          <Movies movies={moviesProp} />
          <div className="Row">
            <div className="Column">
              <button className="button" onClick={this.Previous}>
                Previous
              </button>
            </div>
            <div className="Column">
              <button className="button" onClick={this.Next}>
                Next
              </button>
            </div>
          </div>
          <h2 className="title2">Born Today</h2>
          <Cast cast={castProp} />
        </div>
      );
    }
  }
);

decorate(HomePage, {
  movies: observable,
  pageNumber: observable,
});

export default HomePage;
