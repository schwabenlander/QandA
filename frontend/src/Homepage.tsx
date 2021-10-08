import React from 'react';
import { QuestionList } from './QuestionList';
import { getUnansweredQuestions } from './QuestionData';
import { Page } from './Page';
import { PageTile } from './PageTitle';

export const Homepage = () => (
  <Page>
    <div>
      <PageTile>Unanswered Questions</PageTile>
      <button>Ask a question</button>
    </div>
    <QuestionList data={getUnansweredQuestions()} />
  </Page>
);
