import { gql } from '@apollo/client';
import * as Apollo from '@apollo/client';
export type Maybe<T> = T | null;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
const defaultOptions =  {}
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
};

export type ArticlePageModelType = {
  __typename?: 'ArticlePageModelType';
  heading: Scalars['String'];
  preamble: Scalars['String'];
  text: Scalars['String'];
  url: Scalars['String'];
};

export type BreadcrumbType = {
  __typename?: 'BreadcrumbType';
  text: Scalars['String'];
  url: Scalars['String'];
};

export type BreadcrumbsModelType = {
  __typename?: 'BreadcrumbsModelType';
  breadcrumbs: Array<BreadcrumbType>;
  name: Scalars['String'];
};

export enum ContextModelState {
  Found = 'FOUND',
  NotFound = 'NOT_FOUND'
}

export type ContextModelType = {
  __typename?: 'ContextModelType';
  modelType: Scalars['String'];
  pageId: Scalars['Int'];
  state: ContextModelState;
  url: Scalars['String'];
};

export type NavigationItemType = {
  __typename?: 'NavigationItemType';
  items?: Maybe<Array<Maybe<NavigationItemType>>>;
  text: Scalars['String'];
  url: Scalars['String'];
};

export type NavigationModelType = {
  __typename?: 'NavigationModelType';
  accessibilityDescription: Scalars['String'];
  closeNavigationItemLabel: Scalars['String'];
  closeNavigationPaneLabel: Scalars['String'];
  items?: Maybe<Array<Maybe<NavigationItemType>>>;
  openNavigationItemLabel: Scalars['String'];
  openNavigationPaneLabel: Scalars['String'];
};

export type Query = {
  __typename?: 'Query';
  articlePage?: Maybe<ArticlePageModelType>;
  articles: Array<ArticlePageModelType>;
  breadcrumbs?: Maybe<BreadcrumbsModelType>;
  contextModel?: Maybe<ContextModelType>;
  navigation?: Maybe<NavigationModelType>;
  startPage?: Maybe<StartPageModelType>;
};


export type QueryArticlePageArgs = {
  id: Scalars['Int'];
};


export type QueryArticlesArgs = {
  parentId: Scalars['Int'];
};


export type QueryBreadcrumbsArgs = {
  forPageId: Scalars['Int'];
};


export type QueryContextModelArgs = {
  url: Scalars['String'];
};


export type QueryNavigationArgs = {
  fromPageId?: Maybe<Scalars['Int']>;
};


export type QueryStartPageArgs = {
  id: Scalars['Int'];
};

export type StartPageModelType = {
  __typename?: 'StartPageModelType';
  heading: Scalars['String'];
  preamble: Scalars['String'];
};

export type GetArticlePageQueryVariables = Exact<{
  id: Scalars['Int'];
}>;


export type GetArticlePageQuery = { __typename?: 'Query', articlePage?: Maybe<{ __typename?: 'ArticlePageModelType', heading: string, preamble: string, text: string }> };

export type GetArticlesQueryVariables = Exact<{
  parentId: Scalars['Int'];
}>;


export type GetArticlesQuery = { __typename?: 'Query', articles: Array<{ __typename?: 'ArticlePageModelType', url: string, heading: string, preamble: string }> };

export type GetBreadcrumbsQueryVariables = Exact<{
  forPageId: Scalars['Int'];
}>;


export type GetBreadcrumbsQuery = { __typename?: 'Query', breadcrumbs?: Maybe<{ __typename?: 'BreadcrumbsModelType', breadcrumbs: Array<{ __typename?: 'BreadcrumbType', text: string, url: string }> }> };

export type GetContextModelQueryVariables = Exact<{
  url: Scalars['String'];
}>;


export type GetContextModelQuery = { __typename?: 'Query', contextModel?: Maybe<{ __typename?: 'ContextModelType', pageId: number, modelType: string, state: ContextModelState }> };

export type GetStartPageQueryVariables = Exact<{
  id: Scalars['Int'];
}>;


export type GetStartPageQuery = { __typename?: 'Query', startPage?: Maybe<{ __typename?: 'StartPageModelType', heading: string, preamble: string }>, articles: Array<{ __typename?: 'ArticlePageModelType', url: string, heading: string, preamble: string }> };


export const GetArticlePageDocument = gql`
    query getArticlePage($id: Int!) {
  articlePage(id: $id) {
    heading
    preamble
    text
  }
}
    `;

/**
 * __useGetArticlePageQuery__
 *
 * To run a query within a React component, call `useGetArticlePageQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetArticlePageQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetArticlePageQuery({
 *   variables: {
 *      id: // value for 'id'
 *   },
 * });
 */
export function useGetArticlePageQuery(baseOptions: Apollo.QueryHookOptions<GetArticlePageQuery, GetArticlePageQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetArticlePageQuery, GetArticlePageQueryVariables>(GetArticlePageDocument, options);
      }
export function useGetArticlePageLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetArticlePageQuery, GetArticlePageQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetArticlePageQuery, GetArticlePageQueryVariables>(GetArticlePageDocument, options);
        }
export type GetArticlePageQueryHookResult = ReturnType<typeof useGetArticlePageQuery>;
export type GetArticlePageLazyQueryHookResult = ReturnType<typeof useGetArticlePageLazyQuery>;
export type GetArticlePageQueryResult = Apollo.QueryResult<GetArticlePageQuery, GetArticlePageQueryVariables>;
export const GetArticlesDocument = gql`
    query getArticles($parentId: Int!) {
  articles(parentId: $parentId) {
    url
    heading
    preamble
  }
}
    `;

/**
 * __useGetArticlesQuery__
 *
 * To run a query within a React component, call `useGetArticlesQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetArticlesQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetArticlesQuery({
 *   variables: {
 *      parentId: // value for 'parentId'
 *   },
 * });
 */
export function useGetArticlesQuery(baseOptions: Apollo.QueryHookOptions<GetArticlesQuery, GetArticlesQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetArticlesQuery, GetArticlesQueryVariables>(GetArticlesDocument, options);
      }
export function useGetArticlesLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetArticlesQuery, GetArticlesQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetArticlesQuery, GetArticlesQueryVariables>(GetArticlesDocument, options);
        }
export type GetArticlesQueryHookResult = ReturnType<typeof useGetArticlesQuery>;
export type GetArticlesLazyQueryHookResult = ReturnType<typeof useGetArticlesLazyQuery>;
export type GetArticlesQueryResult = Apollo.QueryResult<GetArticlesQuery, GetArticlesQueryVariables>;
export const GetBreadcrumbsDocument = gql`
    query getBreadcrumbs($forPageId: Int!) {
  breadcrumbs(forPageId: $forPageId) {
    breadcrumbs {
      text
      url
    }
  }
}
    `;

/**
 * __useGetBreadcrumbsQuery__
 *
 * To run a query within a React component, call `useGetBreadcrumbsQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetBreadcrumbsQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetBreadcrumbsQuery({
 *   variables: {
 *      forPageId: // value for 'forPageId'
 *   },
 * });
 */
export function useGetBreadcrumbsQuery(baseOptions: Apollo.QueryHookOptions<GetBreadcrumbsQuery, GetBreadcrumbsQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetBreadcrumbsQuery, GetBreadcrumbsQueryVariables>(GetBreadcrumbsDocument, options);
      }
export function useGetBreadcrumbsLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetBreadcrumbsQuery, GetBreadcrumbsQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetBreadcrumbsQuery, GetBreadcrumbsQueryVariables>(GetBreadcrumbsDocument, options);
        }
export type GetBreadcrumbsQueryHookResult = ReturnType<typeof useGetBreadcrumbsQuery>;
export type GetBreadcrumbsLazyQueryHookResult = ReturnType<typeof useGetBreadcrumbsLazyQuery>;
export type GetBreadcrumbsQueryResult = Apollo.QueryResult<GetBreadcrumbsQuery, GetBreadcrumbsQueryVariables>;
export const GetContextModelDocument = gql`
    query getContextModel($url: String!) {
  contextModel(url: $url) {
    pageId
    modelType
    state
  }
}
    `;

/**
 * __useGetContextModelQuery__
 *
 * To run a query within a React component, call `useGetContextModelQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetContextModelQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetContextModelQuery({
 *   variables: {
 *      url: // value for 'url'
 *   },
 * });
 */
export function useGetContextModelQuery(baseOptions: Apollo.QueryHookOptions<GetContextModelQuery, GetContextModelQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetContextModelQuery, GetContextModelQueryVariables>(GetContextModelDocument, options);
      }
export function useGetContextModelLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetContextModelQuery, GetContextModelQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetContextModelQuery, GetContextModelQueryVariables>(GetContextModelDocument, options);
        }
export type GetContextModelQueryHookResult = ReturnType<typeof useGetContextModelQuery>;
export type GetContextModelLazyQueryHookResult = ReturnType<typeof useGetContextModelLazyQuery>;
export type GetContextModelQueryResult = Apollo.QueryResult<GetContextModelQuery, GetContextModelQueryVariables>;
export const GetStartPageDocument = gql`
    query getStartPage($id: Int!) {
  startPage(id: $id) {
    heading
    preamble
  }
  articles(parentId: $id) {
    url
    heading
    preamble
  }
}
    `;

/**
 * __useGetStartPageQuery__
 *
 * To run a query within a React component, call `useGetStartPageQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetStartPageQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetStartPageQuery({
 *   variables: {
 *      id: // value for 'id'
 *   },
 * });
 */
export function useGetStartPageQuery(baseOptions: Apollo.QueryHookOptions<GetStartPageQuery, GetStartPageQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetStartPageQuery, GetStartPageQueryVariables>(GetStartPageDocument, options);
      }
export function useGetStartPageLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetStartPageQuery, GetStartPageQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetStartPageQuery, GetStartPageQueryVariables>(GetStartPageDocument, options);
        }
export type GetStartPageQueryHookResult = ReturnType<typeof useGetStartPageQuery>;
export type GetStartPageLazyQueryHookResult = ReturnType<typeof useGetStartPageLazyQuery>;
export type GetStartPageQueryResult = Apollo.QueryResult<GetStartPageQuery, GetStartPageQueryVariables>;