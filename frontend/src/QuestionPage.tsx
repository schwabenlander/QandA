/** @jsxImportSource @emotion/react */
import React from 'react';
import { Page } from './Page';
import { useParams } from 'react-router-dom';
import { QuestionData, getQuestion } from './QuestionData';
import { css } from '@emotion/react';
import { gray3, gray6 } from './Styles';

export const QuestionPage = () => {
  const [question, setQuestion] = React.useState<QuestionData | null>(null);

  const { questionId } = useParams();

  React.useEffect(() => {
    const doGetQuestion = async (questionId: number) => {
      const foundQuestion = await getQuestion(questionId);
      setQuestion(foundQuestion);
    };

    if (questionId) {
      doGetQuestion(Number(questionId));
    }
  }, [questionId]);

  return (
    <Page>
      <div
        css={css`
          background-color: #fff;
          padding: 15px 20px 20px 20px;
          border-radius: 4px;
          border: 1px solid ${gray6};
          box-shadow: 0 3px 5px 0 rgba(0, 0, 0, 0.16);
        `}
      >
        <div
          css={css`
            font-size: 19px;
            font-weight: bold;
            margin: 10px 0 5px;
          `}
        >
          {question === null ? '' : question.title}
        </div>
        {question !== null && (
          <React.Fragment>
            <p
              css={css`
                margin-top: 0;
                background-color: #fff;
              `}
            >
              {question.content}
            </p>
            <div
              css={css`
                font-size: 12px;
                font-style: italic;
                color: ${gray3};
              `}
            >
              {`Asked by ${question.userName} on 
                    ${question.created.toLocaleDateString()}
                    ${question.created.toLocaleTimeString()}`}
            </div>
          </React.Fragment>
        )}
      </div>
    </Page>
  );
};
