import React, { Component } from "react";
import Movie from "./Movie";
import "./Movie.scss";

class Movies extends Component {
  render() {
    let movies = this.props.movies.map((movie) => {
      return (
        <Movie
          button={this.props.button}
          buttonTitle={this.props.buttonTitle}
          key={movie.MovieID}
          movie={movie}
        />
      );
    });
    return <div className="moviesDiv">{movies}</div>;
  }
}

export default Movies;
