import React from "react";
import "./styles/LoadingSpinner.css";

const LoadingSpinner: React.FC = () => {
  return (
    <div className='loading-container'>
      <span className='loading-text'>Loading...</span>
    </div>
  );
};

export default LoadingSpinner;
