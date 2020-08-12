import React, { Component } from "react";

class Movie extends Component {
  render() {
    return (
      <div>
        <p>{this.props.movie.Title}</p>
      </div>
    );
  }
}

export default Movie;
