import { useQuery } from "@tanstack/react-query"
import { isNil } from "lodash"
import { FunctionComponent, useCallback } from "react"
import { api } from "../../../../api/apiClient"
import { queryKeys } from "../../../../api/queryKeys"
import formatCDNImageUrl from "../../../../utils/format/formatCDNImageUrl"
import PreferencesPickerItem from "./PreferencesPickerItem"
import { PreferencesPickerWrapper } from "./styles"

type PreferencesPickerProps = {
  selectedPreferences: number[]
  setSelectedPreferences: (preferences: number[]) => void
}

const ONBOARDING_IMAGE_MAX_WIDTH = 384

const PreferencesPicker: FunctionComponent<PreferencesPickerProps> = ({
  selectedPreferences,
  setSelectedPreferences,
}) => {
  const { data } = useQuery(
    [queryKeys.photo.listCategories],
    () =>
      api.photo.listCategories({
        pageNumber: 1,
        pageSize: 100, // pagination is not necessary here
      }),
    {
      refetchOnWindowFocus: false,
    }
  )

  const onPreferenceSelect = useCallback(
    (id: number | undefined) => {
      if (isNil(id)) {
        return
      }

      if (selectedPreferences.includes(id)) {
        setSelectedPreferences(
          selectedPreferences.filter((preferenceId) => preferenceId !== id)
        )
      } else {
        setSelectedPreferences([...selectedPreferences, id])
      }
    },
    [selectedPreferences, setSelectedPreferences]
  )

  return (
    <PreferencesPickerWrapper>
      {data?.value?.data?.map(
        (category) =>
          !isNil(category.id) && (
            <PreferencesPickerItem
              key={category.id}
              active={selectedPreferences.includes(category.id)}
              imageUrl={
                category.imageUri
                  ? formatCDNImageUrl(category.imageUri, {
                      maxWidth: ONBOARDING_IMAGE_MAX_WIDTH,
                    })
                  : ""
              }
              title={category.title ?? "-"}
              onClick={() => onPreferenceSelect(category.id)}
            />
          )
      )}
    </PreferencesPickerWrapper>
  )
}

export default PreferencesPicker
