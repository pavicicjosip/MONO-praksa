import React, { Component } from "react";
import Movies from "../Movie/Movies";

class MovieList extends Component {
  render() {
    return <Movies movies={this.props.movies} />;
  }
}

export default MovieList;
