import React, { Component } from "react";
import axios from "axios";
import Movies from "../Movie/Movies";
import { decorate, observable } from "mobx";
import { observer } from "mobx-react";
import "./HomePage.scss";
import { Link } from "react-router-dom";

const HomePage = observer(
  class HomePage extends Component {
    movies = [];
    pageNumber = 1;

    componentDidMount() {
      this.getMovies();
    }
    Next = () => {
      if ((this.pageNumber + 1) * 4 < this.movies.m_Item1 + 4) {
        this.pageNumber++;
        this.getMovies();
      }
    };

    Previous = () => {
      if (this.pageNumber !== 1) {
        this.pageNumber--;
        this.getMovies();
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

    render() {
      let moviesProp = [];
      if (this.movies.length !== 0) {
        moviesProp = this.movies.m_Item2;
      }
      return (
        <div>
          <p>
            <img src={require("./b.png")} className="center" alt="" />
          </p>
          <h1 className="centerd">Welcome...</h1>
          <h3 className="centerd1">
            You've found the most accurate source for TV and film. Our
            information comes from fans like you, so create a free account and
            help your favorite shows and movies.
          </h3>
          <h2 className="title">Featured Movies</h2>
          <div className="spacer"></div>
          <button onClick={this.Next}>Next</button>
          <Movies movies={moviesProp} />
          <button onClick={this.Previous}>Previous</button>
          <div>
            <Link to="/movieInfoPage/99120BAA-5B4C-46CB-A1A2-0805930A0EE9">
              PROBA FILM
            </Link>
          </div>
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
