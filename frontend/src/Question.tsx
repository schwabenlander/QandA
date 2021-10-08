import React from 'react';
import { QuestionData } from './QuestionData';

interface Props {
  data: QuestionData;
  showContent?: boolean;
}

export const Question = ({ data, showContent = true }: Props) => (
  <div>
    <div>{data.title}</div>
    {showContent && (
      <div>
        {data.content.length > 50
          ? `${data.content.substring(0, 50)}...`
          : data.content}
      </div>
    )}
    <div>
      {`Asked by ${data.userName} on 
            ${data.created.toLocaleDateString()} at ${data.created.toLocaleTimeString()}`}
    </div>
  </div>
);
