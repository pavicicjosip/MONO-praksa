import React, { Component } from "react";
import Movie from "./Movie";
import "./Movie.scss";

class Movies extends Component {
  render() {
    console.log(this.props.movies);
    let movies = this.props.movies.map((movie) => {
      return <Movie key={movie.MovieID} movie={movie} />;
    });
    return <div className="moviesDiv">{movies}</div>;
  }
}

export default Movies;
