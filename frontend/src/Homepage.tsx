import React from 'react';
import { QuestionList } from './QuestionList';
import { getUnansweredQuestions } from './QuestionData';

export const Homepage = () => (
  <div>
    <div>
      <h2>Unanswered Questions</h2>
      <button>Ask a question</button>
    </div>
    <QuestionList data={getUnansweredQuestions()} />
  </div>
);
