import React, { Component } from "react";
import Movies from "../Movie/Movies";

class MovieList extends Component {
  render() {
    return (
      <div>
        <Movies
          buttonTitle="Remove"
          button={this.props.removeFromList}
          movies={this.props.movies}
        />
        {this.props.movies.length !== 0 ? (
          <div>
            <button className="button" onClick={this.props.Previous}>
              Previous
            </button>
            <button className="button" onClick={this.props.Next}>
              Next
            </button>
          </div>
        ) : null}
      </div>
    );
  }
}

export default MovieList;
