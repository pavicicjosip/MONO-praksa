import React, { Component } from "react";

class MovieLists extends Component {
  render() {
    let lists = this.props.lists.map((list) => {
      return <p key={list.ListName}>{list.ListName}</p>;
    });
    return <div>{lists}</div>;
  }
}

export default MovieLists;
