/** @jsxImportSource @emotion/react */
import React from 'react';
import { QuestionData } from './QuestionData';
import { css } from '@emotion/react';
import { gray2, gray3 } from './Styles';

interface Props {
  data: QuestionData;
  showContent?: boolean;
}

export const Question = ({ data, showContent = true }: Props) => (
  <div
    css={css`
      padding: 10px 0;
    `}
  >
    <div
      css={css`
        padding: 10px 0;
        font-size: 22px;
      `}
    >
      {data.title}
    </div>
    {showContent && (
      <div
        css={css`
          padding-bottom: 10px;
          font-size: 16px;
          color: ${gray2};
        `}
      >
        {data.content.length > 50
          ? `${data.content.substring(0, 50)}...`
          : data.content}
      </div>
    )}
    <div
      css={css`
        font-size: 12px;
        font-style: italic;
        color: ${gray3};
      `}
    >
      {`Asked by ${data.userName} on 
            ${data.created.toLocaleDateString()} at ${data.created.toLocaleTimeString()}`}
    </div>
  </div>
);
