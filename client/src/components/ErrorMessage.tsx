import React from "react";
import "./styles/ErrorMessage.css";

interface ErrorMessageProps {
  message: string;
}

const ErrorMessage: React.FC<ErrorMessageProps> = ({ message }) => {
  return (
    <div className='error-message'>
      <p>Error: {message}</p>
    </div>
  );
};

export default ErrorMessage;
