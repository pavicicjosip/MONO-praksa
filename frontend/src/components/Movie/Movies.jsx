import React, { Component } from "react";
import Movie from "./Movie";

class Movies extends Component {
  render() {
    let movies = this.props.movies.map((movie) => {
      return <Movie key={movie.MovieID} movie={movie} />;
    });
    return <div>{movies}</div>;
  }
}

export default Movies;
