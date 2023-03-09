import {
  Accordion,
  AccordionButton,
  AccordionItem,
  AccordionPanel,
} from "@chakra-ui/react"
import { PrismicRichText, SliceComponentProps } from "@prismicio/react"
import { FunctionComponent, useState } from "react"
import { FaqsSectionSlice } from "../../types.generated"
import useIsMobile from "../../utils/hooks/useIsMobile"
import {
  MobileDivider,
  MobileIconNotSelected,
  MobileIconSelected,
  MobileQuestionContent,
  MobileQuestionItemWrapper,
  MobileQuestionNumber,
  MobileQuestionTitle,
  MobileQuestionTitleWrapper,
} from "../FAQsMobileSection/QuestionItem/styles"
import { FAQsMobileSectionWrapper } from "../FAQsMobileSection/styles"
import {
  Divider,
  IconNotSelected,
  IconSelected,
  QuestionContent,
  QuestionItemWrapper,
  QuestionNumber,
  QuestionTitle,
} from "./QuestionItem/styles"
import { FAQsSectionWrapper } from "./styles"

type FAQsSectionProps = SliceComponentProps<FaqsSectionSlice>

const FaqsSection: FunctionComponent<FAQsSectionProps> = ({ slice }) => {
  const [selectedQuestion, setSelectedQuestion] = useState(0)
  const isHoveredFirstQuestionHovered: boolean = selectedQuestion === 1
  const isHoveredSecondQuestionHovered: boolean = selectedQuestion === 2
  const isHoveredThirdQuestionHovered: boolean = selectedQuestion === 3

  const isMobile = useIsMobile()

  if (isMobile) {
    return (
      <FAQsMobileSectionWrapper>
        <Accordion allowToggle index={selectedQuestion - 1}>
          <AccordionItem border={0}>
            <AccordionButton
              padding={0}
              onClick={() => {
                setSelectedQuestion(selectedQuestion === 1 ? 0 : 1)
              }}
            >
              <MobileQuestionItemWrapper
                isHovered={isHoveredFirstQuestionHovered}
              >
                <MobileQuestionTitleWrapper>
                  <MobileQuestionNumber
                    isHovered={isHoveredFirstQuestionHovered}
                  >
                    01.
                  </MobileQuestionNumber>
                  <MobileQuestionTitle
                    isHovered={isHoveredFirstQuestionHovered}
                  >
                    {slice.primary.FirstQuestionTitle && (
                      <PrismicRichText
                        field={slice.primary.FirstQuestionTitle}
                      />
                    )}
                  </MobileQuestionTitle>
                </MobileQuestionTitleWrapper>

                {isHoveredFirstQuestionHovered ? (
                  <MobileIconSelected />
                ) : (
                  <MobileIconNotSelected />
                )}
              </MobileQuestionItemWrapper>
            </AccordionButton>
            <AccordionPanel padding={0}>
              <MobileQuestionContent isHovered={isHoveredFirstQuestionHovered}>
                {slice.primary.FirstQuestionDescription && (
                  <PrismicRichText
                    field={slice.primary.FirstQuestionDescription}
                  />
                )}
              </MobileQuestionContent>
            </AccordionPanel>
          </AccordionItem>

          <MobileDivider noMargin={false} />

          <AccordionItem border={0}>
            <AccordionButton
              padding={0}
              onClick={() => {
                setSelectedQuestion(selectedQuestion === 2 ? 0 : 2)
              }}
            >
              <MobileQuestionItemWrapper
                isHovered={isHoveredSecondQuestionHovered}
              >
                <MobileQuestionTitleWrapper>
                  <MobileQuestionNumber
                    isHovered={isHoveredSecondQuestionHovered}
                  >
                    02.
                  </MobileQuestionNumber>
                  <MobileQuestionTitle
                    isHovered={isHoveredSecondQuestionHovered}
                  >
                    {slice.primary.SecondQuestionTitile && (
                      <PrismicRichText
                        field={slice.primary.SecondQuestionTitile}
                      />
                    )}
                  </MobileQuestionTitle>
                </MobileQuestionTitleWrapper>
                {isHoveredSecondQuestionHovered ? (
                  <MobileIconSelected />
                ) : (
                  <MobileIconNotSelected />
                )}
              </MobileQuestionItemWrapper>
            </AccordionButton>
            <AccordionPanel padding={0}>
              <MobileQuestionContent isHovered={isHoveredSecondQuestionHovered}>
                {slice.primary.SecondQuestionDescription && (
                  <PrismicRichText
                    field={slice.primary.SecondQuestionDescription}
                  />
                )}
              </MobileQuestionContent>
            </AccordionPanel>
          </AccordionItem>

          <MobileDivider noMargin={false} />

          <AccordionItem border={0}>
            <AccordionButton
              padding={0}
              onClick={() => {
                setSelectedQuestion(selectedQuestion === 3 ? 0 : 3)
              }}
            >
              <MobileQuestionItemWrapper
                isHovered={isHoveredThirdQuestionHovered}
              >
                <MobileQuestionTitleWrapper>
                  <MobileQuestionNumber
                    isHovered={isHoveredThirdQuestionHovered}
                  >
                    03.
                  </MobileQuestionNumber>
                  <MobileQuestionTitle
                    isHovered={isHoveredThirdQuestionHovered}
                  >
                    {slice.primary.ThirdQuestionTitle && (
                      <PrismicRichText
                        field={slice.primary.ThirdQuestionTitle}
                      />
                    )}
                  </MobileQuestionTitle>
                </MobileQuestionTitleWrapper>

                {isHoveredThirdQuestionHovered ? (
                  <MobileIconSelected />
                ) : (
                  <MobileIconNotSelected />
                )}
              </MobileQuestionItemWrapper>
            </AccordionButton>
            <AccordionPanel padding={0}>
              <MobileQuestionContent isHovered={isHoveredThirdQuestionHovered}>
                {slice.primary.ThirdQuestionDescription && (
                  <PrismicRichText
                    field={slice.primary.ThirdQuestionDescription}
                  />
                )}
              </MobileQuestionContent>
            </AccordionPanel>
          </AccordionItem>
          <MobileDivider noMargin={false} />
        </Accordion>
      </FAQsMobileSectionWrapper>
    )
  }

  return (
    <FAQsSectionWrapper
      onMouseLeave={() => {
        setSelectedQuestion(0)
      }}
    >
      <div id="question">
        <QuestionItemWrapper
          isHovered={isHoveredFirstQuestionHovered}
          onMouseEnter={() => {
            setSelectedQuestion(1)
          }}
        >
          <QuestionNumber isHovered={isHoveredFirstQuestionHovered}>
            01.
          </QuestionNumber>
          <QuestionTitle isHovered={isHoveredFirstQuestionHovered}>
            {slice.primary.FirstQuestionDescription && (
              <PrismicRichText field={slice.primary.FirstQuestionTitle} />
            )}
          </QuestionTitle>
          <QuestionContent isHovered={isHoveredFirstQuestionHovered}>
            {slice.primary.FirstQuestionDescription && (
              <PrismicRichText field={slice.primary.FirstQuestionDescription} />
            )}
          </QuestionContent>
          {isHoveredFirstQuestionHovered ? (
            <IconSelected />
          ) : (
            <IconNotSelected />
          )}
        </QuestionItemWrapper>
        <Divider />
      </div>

      <div id="question">
        <QuestionItemWrapper
          isHovered={isHoveredSecondQuestionHovered}
          onMouseEnter={() => {
            setSelectedQuestion(2)
          }}
        >
          <QuestionNumber isHovered={isHoveredSecondQuestionHovered}>
            02.
          </QuestionNumber>
          <QuestionTitle isHovered={isHoveredSecondQuestionHovered}>
            {slice.primary.SecondQuestionTitile && (
              <PrismicRichText field={slice.primary.SecondQuestionTitile} />
            )}
          </QuestionTitle>
          <QuestionContent isHovered={isHoveredSecondQuestionHovered}>
            {slice.primary.SecondQuestionDescription && (
              <PrismicRichText
                field={slice.primary.SecondQuestionDescription}
              />
            )}
          </QuestionContent>
          {isHoveredSecondQuestionHovered ? (
            <IconSelected />
          ) : (
            <IconNotSelected />
          )}
        </QuestionItemWrapper>
        <Divider />
      </div>

      <div id="question">
        <QuestionItemWrapper
          isHovered={isHoveredThirdQuestionHovered}
          onMouseEnter={() => {
            setSelectedQuestion(3)
          }}
        >
          <QuestionNumber isHovered={isHoveredThirdQuestionHovered}>
            03.
          </QuestionNumber>
          <QuestionTitle isHovered={isHoveredThirdQuestionHovered}>
            {slice.primary.ThirdQuestionTitle && (
              <PrismicRichText field={slice.primary.ThirdQuestionTitle} />
            )}
          </QuestionTitle>
          <QuestionContent isHovered={isHoveredThirdQuestionHovered}>
            {slice.primary.ThirdQuestionDescription && (
              <PrismicRichText field={slice.primary.ThirdQuestionDescription} />
            )}
          </QuestionContent>
          {isHoveredThirdQuestionHovered ? (
            <IconSelected />
          ) : (
            <IconNotSelected />
          )}
        </QuestionItemWrapper>
        <Divider />
      </div>
    </FAQsSectionWrapper>
  )
}

export default FaqsSection
