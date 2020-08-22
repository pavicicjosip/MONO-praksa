import React, { Component } from "react";
import "./MovieInfoPage.scss";
import "./MovieInfoPage.scss";

class Comments extends Component {
  render() {
    let commentsProp = [];
    if (this.props.comments.length !== 0) {
      commentsProp = this.props.comments.m_Item2;
    }

    commentsProp = commentsProp.map((m) => {
      return (
        <div key={m.ReviewID}>
          <div className="user">{m.Username}</div>
          <div className="comment">{m.Comment}</div>
        </div>
      );
    });

    return <div>{commentsProp}</div>;
  }
}

export default Comments;
