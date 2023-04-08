import React from "react";
import "./styles/LoadingSpinner.css";

const LoadingSpinner: React.FC = () => {
  return (
    <div className='loadingContainer'>
      <span className='loadingText'>Loading...</span>
    </div>
  );
};

export default LoadingSpinner;
