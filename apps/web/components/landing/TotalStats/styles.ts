import { down } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const TotalStatsWrapper = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;

  min-height: ${rem(156)};
  margin-top: ${({ theme }) => theme.sizes.xxxxlarge};
  margin-bottom: ${({ theme }) => theme.sizes.xxxlarge};
  padding-right: 8vw;
  padding-left: 8vw;

  color: ${({ theme }) => theme.colors.primary};

  background-color: ${({ theme }) => theme.colors.background2};

  ${down("md")} {
    min-height: ${rem(106)};
    margin-top: ${({ theme }) => theme.sizes.large};
    margin-bottom: ${({ theme }) => theme.sizes.medium};
  }

  ${down("sm")} {
    flex-direction: column;

    margin-top: ${({ theme }) => theme.sizes.medium};
    margin-bottom: ${({ theme }) => theme.sizes.medium};
    padding-top: ${({ theme }) => theme.sizes.medium};
    padding-bottom: ${({ theme }) => theme.sizes.medium};
  }
`

export const CircularSeparator = styled.div`
  display: inline-block;

  width: ${({ theme }) => theme.sizes.xlarge};
  height: ${({ theme }) => theme.sizes.xlarge};

  background-color: ${({ theme }) => theme.colors.secondary};
  border-radius: 50%;

  ${down("md")} {
    width: ${({ theme }) => theme.sizes.large};
    height: ${({ theme }) => theme.sizes.large};
  }
`

export const StatsItem = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;

  ${down("sm")} {
    margin-top: ${({ theme }) => theme.sizes.large};
    margin-bottom: ${({ theme }) => theme.sizes.large};
  }
`

export const StatsItemTop = styled.div`
  font-size: ${({ theme }) => theme.sizes.xxlarge};
  font-weight: 800;

  ${down("md")} {
    font-size: ${({ theme }) => theme.sizes.xlarge};
  }

  ${down("sm")} {
    font-size: ${({ theme }) => theme.sizes.xlarge};
  }
`

export const StatsItemBottom = styled.div`
  font-size: ${({ theme }) => theme.sizes.medium};
  font-weight: 600;

  ${down("md")} {
    font-size: ${({ theme }) => theme.sizes.small};
  }
  ${down("sm")} {
    font-size: ${({ theme }) => theme.sizes.medium};
  }
`
